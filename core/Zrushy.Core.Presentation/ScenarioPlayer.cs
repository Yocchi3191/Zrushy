using System;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Presentation
{
	public class ScenarioPlayer
	{
		private readonly IScenarioEngine engine;
		private readonly HeroinViewModel heroin;

		public bool IsPlaying => isPlaying;
		private bool isPlaying = false;
		private bool pendingNewLine = false;

		/// <summary>
		/// シナリオが開始されたときに発火するイベント
		/// ScenarioDriver などが購読して自動進行を開始する
		/// </summary>
		public event System.Action? OnScenarioStarted;

		/// <summary>
		/// シナリオが終了したときに発火するイベント
		/// ScenarioDriver などが購読して自動進行を停止する
		/// </summary>
		public event System.Action? OnScenarioFinished;

		public ScenarioPlayer(IScenarioEngine engine, HeroinViewModel heroin)
		{
			this.engine = engine;
			this.heroin = heroin;
		}

		public void Play(ScenarioID scenarioID)
		{
			if (isPlaying)
			{
				return;
			}

			try
			{
				engine.Start(scenarioID);
				isPlaying = true;
				heroin.Act(engine.GetCurrentAction());

				// シナリオ開始イベントを発火
				OnScenarioStarted?.Invoke();
			}
			catch (ScenarioNotFoundException ex)
			{
				// シナリオが見つからない場合はログを出力して処理を中断
				// isPlaying は false のままにする
				Console.WriteLine($"[ScenarioPlayer] {ex.Message}");
				throw;
			}
		}

		public void Next()
		{
			if (!isPlaying)
				return;

			// 前回の advance で受け取った新ラインの entry 条件が未達の場合、再度チェック
			if (pendingNewLine)
			{
				var condition = engine.CurrentProceedCondition;
				if (condition != null && !condition.CanFire())
					return;

				pendingNewLine = false;
				heroin.Act(engine.GetCurrentAction());
				return;
			}

			engine.Next();
			if (engine.IsScenarioFinished)
			{
				isPlaying = false;
				OnScenarioFinished?.Invoke();
				return;
			}

			// 新ラインの entry 条件を確認
			var entryCondition = engine.CurrentProceedCondition;
			if (entryCondition != null && !entryCondition.CanFire())
			{
				// 条件未達 — Yarn は次のラインを保持したまま待機
				pendingNewLine = true;
				return;
			}

			heroin.Act(engine.GetCurrentAction());
		}
	}
}
