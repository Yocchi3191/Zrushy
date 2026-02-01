using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Domain.Entity
{
	public interface IScenarioEngine
	{
		bool IsScenarioFinished { get; }

		Action GetCurrentAction();
		void Next();
		void Start(ScenarioID scenarioID);
	}
}
