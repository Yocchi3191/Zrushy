using System;
using Zrushy.Core.Application;
using Zrushy.Core.Application.UseCase.GetScenario;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Presentation
{
    /// <summary>
    /// シナリオの開始・進行を管理するクラス
    /// </summary>
    public class ScenarioPlayer : IScenarioAdvancable
    {
        private readonly GetScenario getScenario;
        private readonly HeroinViewModel heroin;
        private readonly EventBus _eventBus;
        private readonly IBeatProvidable beatProvider;

        public bool IsPlaying => isPlaying;
        private bool isPlaying = false;
        /// <summary>
        /// シナリオが開始されたときに発火するイベント
        /// ScenarioDriver などが購読して自動進行を開始する
        /// </summary>
        public event Action? OnScenarioStarted;

        /// <summary>
        /// シナリオが終了したときに発火するイベント
        /// ScenarioDriver などが購読して自動進行を停止する
        /// </summary>
        public event Action? OnScenarioFinished;

        public ScenarioPlayer(GetScenario getScenario,
            HeroinViewModel heroin,
            EventBus eventBus,
            IBeatProvidable beatProvidable
            )
        {
            this.getScenario = getScenario;
            this.heroin = heroin;
            this.beatProvider = beatProvidable;
            this._eventBus = eventBus;
            _eventBus.OnEventPublished += Play; // Eventが発火したらシナリオ開始

            beatProvidable.OnBeatReady += this.heroin.Act;
            beatProvidable.OnCompleted += FinishScenario;
        }

        public void Play(EventID firedEventID)
        {
            if (isPlaying) return;

            ScenarioID scenarioID = getScenario.Execute(firedEventID);
            beatProvider.Start(scenarioID);
            isPlaying = true;
            OnScenarioStarted?.Invoke();
        }

        public void Next()
        {
            if (!isPlaying) return;
            beatProvider.Advance();
        }

        /// <summary>
        /// 現在再生中のシナリオを強制的に停止する
        /// 高優先度イベント（絶頂など）による割り込み時に使用
        /// </summary>
        public void Stop()
        {
            if (!isPlaying) return;

            FinishScenario();
        }

        private void FinishScenario()
        {
            isPlaying = false;
            OnScenarioFinished?.Invoke();
        }
    }
}
