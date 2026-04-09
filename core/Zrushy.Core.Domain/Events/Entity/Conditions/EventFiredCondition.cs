// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
    /// <summary>
    /// 指定したイベントが過去に発火済みのときに真となる条件
    /// </summary>
    internal class EventFiredCondition : ICondition
    {
        private readonly IFiredEventLog _firedEventLog;
        private readonly EventID _eventID;

        public EventFiredCondition(IFiredEventLog firedEventLog, EventID eventID)
        {
            _firedEventLog = firedEventLog;
            _eventID = eventID;
        }

        public bool CanFire()
        {
            return _firedEventLog.HasFired(_eventID);
        }
    }
}
