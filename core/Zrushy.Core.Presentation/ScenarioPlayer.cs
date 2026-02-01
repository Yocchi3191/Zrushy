using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Presentation
{
	public class ScenarioPlayer
	{
		private readonly IScenarioEngine engine;
		private readonly HeroinViewModel heroinViewModel;
		private bool isPlaying = false;


		public ScenarioPlayer(IScenarioEngine engine, HeroinViewModel heroinViewModel)
		{
			this.engine = engine;
			this.heroinViewModel = heroinViewModel;
		}

		internal void Play(ScenarioID scenarioID)
		{
			if (isPlaying)
			{
				return;
			}

			isPlaying = true;

			engine.Start(scenarioID);
			Action action = engine.GetCurrentAction();

			heroinViewModel.Act(action);
		}

		public void Next()
		{
			if (!isPlaying)
			{
				return;
			}

			if (engine.IsScenarioFinished)
			{
				isPlaying = false;
				return;
			}

			engine.Next();
			Action action = engine.GetCurrentAction();

			heroinViewModel.Act(action);
		}
	}
}
