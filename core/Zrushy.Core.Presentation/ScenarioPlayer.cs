using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Presentation
{
	public class ScenarioPlayer
	{
		private readonly IScenarioEngine engine;
		private readonly HeroinViewModel heroin;

		private bool isPlaying = false;

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
				return;
			}

			heroin.Act(engine.GetCurrentAction());
		}
	}
}
