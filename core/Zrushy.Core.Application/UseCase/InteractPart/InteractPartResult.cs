using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	public class InteractPartResult
	{
		public ScenarioID ScenarioToStart { get; }
		public InteractPartResult(ScenarioID scenarioToStart)
		{
			ScenarioToStart = scenarioToStart;
		}
	}
}
