// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application
{
    /// <summary>
    /// インタラクションに基づいてイベント発火条件を評価し、
    /// 発火対象イベントをイベントバスに送信する
    /// </summary>
    public class EventEvaluator : IEventEvaluator
    {
        private readonly IInteractionHistory interactionHistory;
        private readonly IEventRepository eventRepository;
        private readonly EventBus eventBus;

        public EventEvaluator(
            IInteractionHistory interactionHistory,
            IEventRepository eventRepository,
            EventBus eventBus)
        {
            this.interactionHistory = interactionHistory;
            this.eventRepository = eventRepository;
            this.eventBus = eventBus;
        }

        public void Evaluate(Interaction interaction)
        {
            interactionHistory.Record(interaction);

            var partEvents = eventRepository.GetEvents(interaction.PartID);
            var globalEvents = eventRepository.GetGlobalEvents();

            IScenarioEvent fired = partEvents.Concat(globalEvents)
                .Where(e => e.CanFire())
                .OrderByDescending(e => e.Priority)
                .FirstOrDefault();

            if (fired != null)
                eventBus.Publish(fired);
        }
    }
}
