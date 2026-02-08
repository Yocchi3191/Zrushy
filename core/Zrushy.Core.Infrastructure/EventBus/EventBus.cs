using System;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;

namespace Zrushy.Core.Infrastructure.EventBus
{
	public class EventBus : IEventBus
	{
		private readonly IFiredEventLog firedEventLog;

		public EventBus(IFiredEventLog firedEventLog)
		{
			this.firedEventLog = firedEventLog;
		}

		public event Action<IScenarioEvent>? OnEventPublished;

		public void Publish(IScenarioEvent gameEvent)
		{
			if (gameEvent == null)
				return;

			firedEventLog.Record(gameEvent.ID);
			OnEventPublished?.Invoke(gameEvent);
		}
	}
}
