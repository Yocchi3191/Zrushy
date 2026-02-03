using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity
{
	public interface IEvent
	{
		ScenarioID ScenarioToStart { get; }
		int Priority { get; }
		bool CanFire();
	}
}
