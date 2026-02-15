using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Repository
{
	public interface IScenarioRepository
	{
		bool IsScenarioFinished { get; }
		event System.Action<Action> OnActionChanged;

		Action GetCurrentAction();
		void Next();
		void Start(ScenarioID scenarioID);
		void Stop();
	}
}
