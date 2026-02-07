using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Presentation
{
	public class ScenarioPlayer
	{
		private readonly IScenarioEngine engine;
		private readonly HeroinViewModel heroin;

		private bool isPlaying = false;

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

		internal void Play(ScenarioID scenarioID)
		{
			if (isPlaying)
			{
				return;
			}
			isPlaying = true;

			engine.Start(scenarioID);
			heroin.Act(engine.GetCurrentAction());

			// シナリオ開始イベントを発火
			OnScenarioStarted?.Invoke();
		}

		public void Next()
		{
			if (!isPlaying)
			{
				return;
			}

			engine.Next();
			if (engine.IsScenarioFinished)
			{
				isPlaying = false;

				// シナリオ終了イベントを発火
				OnScenarioFinished?.Invoke();
				return;
			}

			heroin.Act(engine.GetCurrentAction());
		}
	}
}
