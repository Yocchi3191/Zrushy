using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
	public class EventRepository : IEventRepository
	{
		public IReadOnlyList<IScenarioEvent> GetEvents(PartID partID)
		{
			return new List<IScenarioEvent>
			{
				new Event(
					new EventID(partID.ToString() + "_default"),
					new ScenarioID(partID.ToString() + "_default"),
					priority: 0)
			};
		}
	}
}
