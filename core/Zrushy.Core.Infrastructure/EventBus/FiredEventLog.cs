using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Infrastructure.EventBus
{
	public class FiredEventLog : IFiredEventLog
	{
		private readonly HashSet<EventID> firedEvents = new HashSet<EventID>();

		public bool HasFired(EventID eventID)
		{
			return firedEvents.Contains(eventID);
		}

		public void Record(EventID eventID)
		{
			firedEvents.Add(eventID);
		}
	}
}
