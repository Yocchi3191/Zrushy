using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Presentation
{
	public class ScenarioPlayer
	{
		private readonly IScenarioRepository repository;
		private readonly HeroinViewModel heroin;

		private bool isPlaying = false;
		private Scenario? scenario;



		public ScenarioPlayer(IScenarioRepository scenarioReposiory, HeroinViewModel heroinViewModel)
		{
			this.repository = scenarioReposiory;
			this.heroin = heroinViewModel;
		}

		internal void Play(ScenarioID scenarioID)
		{
			if (isPlaying)
			{
				return;
			}
			isPlaying = true;

			scenario = repository.GetScenario(scenarioID);

			heroin.Act(scenario.First());
		}

		public void Next()
		{
			if (!isPlaying)
			{
				return;
			}

			if (scenario.IsScenarioFinished)
			{
				isPlaying = false;
				return;
			}

			heroin.Act(scenario.Next());
		}
	}
}
