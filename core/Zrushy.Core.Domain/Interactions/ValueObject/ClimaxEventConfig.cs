using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	public record ClimaxEventConfig(EventID EventID, ScenarioID ScenarioID, int Priority);
}