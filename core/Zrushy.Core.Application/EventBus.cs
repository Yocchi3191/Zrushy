// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

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
        private readonly IFiredEventLog _firedEventLog;

        public EventBus(IFiredEventLog firedEventLog)
        {
            _firedEventLog = firedEventLog;
        }

        public event Action<EventID>? OnEventPublished;

        public void Publish(IScenarioEvent gameEvent)
        {
            if (gameEvent == null)
            {
                return;
            }

            _firedEventLog.Record(gameEvent.ID);
            OnEventPublished?.Invoke(gameEvent.ID);
        }
    }
}
