// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Linq;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application
{
    public class ClothingEventEvaluator : IClothingEventEvaluator
    {
        private readonly IZrushyHistory _history;
        private readonly IClothingEventRepository _eventRepository;
        private readonly EventBus _eventBus;

        public ClothingEventEvaluator(
            IZrushyHistory history,
            IClothingEventRepository eventRepository,
            EventBus eventBus)
        {
            _history = history;
            _eventRepository = eventRepository;
            _eventBus = eventBus;
        }

        public void Evaluate(ClothingID clothingID, bool isSuccess)
        {
            _history.Record(clothingID, isSuccess);

            IScenarioEvent fired = _eventRepository.GetEvents(clothingID, isSuccess)
                .Where(e => e.CanFire())
                .OrderByDescending(e => e.Priority)
                .FirstOrDefault();

            if (fired != null)
                _eventBus.Publish(fired);
        }
    }
}
