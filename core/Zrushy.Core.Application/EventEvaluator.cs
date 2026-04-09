// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
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
        private readonly IInteractionHistory _interactionHistory;
        private readonly IEventRepository _eventRepository;
        private readonly EventBus _eventBus;

        public EventEvaluator(
            IInteractionHistory interactionHistory,
            IEventRepository eventRepository,
            EventBus eventBus)
        {
            _interactionHistory = interactionHistory;
            _eventRepository = eventRepository;
            _eventBus = eventBus;
        }

        public void Evaluate(Interaction interaction)
        {
            _interactionHistory.Record(interaction);

            IReadOnlyList<IScenarioEvent> partEvents = _eventRepository.GetEvents(interaction.PartID);
            IReadOnlyList<IScenarioEvent> globalEvents = _eventRepository.GetGlobalEvents();

            IScenarioEvent fired = partEvents.Concat(globalEvents)
                .Where(e => e.CanFire())
                .OrderByDescending(e => e.Priority)
                .FirstOrDefault();

            if (fired != null)
            {
                _eventBus.Publish(fired);
            }
        }
    }
}
