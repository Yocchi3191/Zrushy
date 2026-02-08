using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
	/// <summary>
	/// 指定したイベントが過去に発火済みのときに真となる条件
	/// </summary>
	internal class EventFiredCondition : ICondition
	{
		private readonly IFiredEventLog firedEventLog;
		private readonly EventID eventID;

		public EventFiredCondition(IFiredEventLog firedEventLog, EventID eventID)
		{
			this.firedEventLog = firedEventLog;
			this.eventID = eventID;
		}

		public bool CanFire()
		{
			return firedEventLog.HasFired(eventID);
		}
	}
}
