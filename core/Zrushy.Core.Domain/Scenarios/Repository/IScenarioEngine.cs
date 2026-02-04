using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Repository
{
	public interface IScenarioEngine
	{
		bool IsScenarioFinished { get; }

		Action GetCurrentAction();
		void Next();
		void Start(ScenarioID scenarioID);
	}
}
