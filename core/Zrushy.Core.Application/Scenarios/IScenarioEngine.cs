using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Application.Scenarios
{
	public interface IScenarioEngine
	{
		bool IsScenarioFinished { get; }
		event System.Action<Beat> OnBeatChanged;

		Beat GetCurrentBeat();
		void Next();
		void Start(ScenarioID scenarioID);
		void Stop();
	}
}
