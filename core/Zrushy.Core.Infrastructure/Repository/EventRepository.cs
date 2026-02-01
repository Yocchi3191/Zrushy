using System.Collections.Generic;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
	public class EventRepository : IEventRepository
	{
		public IReadOnlyList<IEvent> GetEvents(PartID partID)
		{
			return new List<IEvent>
			{
				new DefaultEvent(new ScenarioID(partID.ToString() + "_default"))
			};
		}
	}

	internal class DefaultEvent : IEvent
	{
		public ScenarioID ScenarioToStart { get; }
		public int Priority => 0;

		public DefaultEvent(ScenarioID scenarioID)
		{
			ScenarioToStart = scenarioID;
		}

		public bool CanFire()
		{
			return true;
		}
	}
}
