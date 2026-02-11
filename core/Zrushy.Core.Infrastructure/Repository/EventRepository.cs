using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
	public class EventRepository : IEventRepository
	{
		public IReadOnlyList<IScenarioEvent> GetEvents(PartID partID)
		{
			return new List<IScenarioEvent>();
		}
	}
}
