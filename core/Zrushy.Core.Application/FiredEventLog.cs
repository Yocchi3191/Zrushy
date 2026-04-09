// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Application
{
    public class FiredEventLog : IFiredEventLog
    {
        private readonly HashSet<EventID> _firedEvents = new HashSet<EventID>();

        public bool HasFired(EventID eventID)
        {
            return _firedEvents.Contains(eventID);
        }

        public void Record(EventID eventID)
        {
            _firedEvents.Add(eventID);
        }
    }
}
