using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Domain.Entity
{
	public interface IEvent
	{
		ScenarioID ScenarioToStart { get; }
		int Priority { get; }
		bool CanFire();
	}
}
