using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity
{

	public class Event : IScenarioEvent
	{
		public EventID ID { get; }
		public ScenarioID ScenarioToStart { get; }
		public int Priority { get; }

		private readonly ICondition[] conditions;

		public Event(EventID id, ScenarioID scenarioToStart, int priority, params ICondition[] conditions)
		{
			ID = id;
			ScenarioToStart = scenarioToStart;
			Priority = priority;
			this.conditions = conditions;
		}

		public bool CanFire()
		{
			foreach (var condition in conditions)
			{
				if (!condition.CanFire())
					return false;
			}
			return true;
		}
	}
}
