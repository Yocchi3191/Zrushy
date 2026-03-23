// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Application
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
