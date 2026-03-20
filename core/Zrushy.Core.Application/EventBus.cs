// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Application
{
    /// <summary>
    /// IScenarioEventの発火を行うクラス
    /// </summary>
	public class EventBus
    {
        private readonly IFiredEventLog firedEventLog;

        public EventBus(IFiredEventLog firedEventLog)
        {
            this.firedEventLog = firedEventLog;
        }

        public event Action<EventID>? OnEventPublished;

        public void Publish(IScenarioEvent gameEvent)
        {
            if (gameEvent == null)
                return;

            firedEventLog.Record(gameEvent.ID);
            OnEventPublished?.Invoke(gameEvent.ID);
        }
    }
}
