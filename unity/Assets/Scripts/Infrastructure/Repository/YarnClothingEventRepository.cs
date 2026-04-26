// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Infrastructure.Unity
{
    public class YarnClothingEventRepository : IClothingEventRepository
    {
        private readonly Dictionary<(ClothingID, SlideResult), List<IScenarioEvent>> _cache = new();

        public YarnClothingEventRepository(DialogueRunner dialogueRunner, IConditionFactory conditionFactory)
        {
            foreach (var pair in dialogueRunner.YarnProject.Program.Nodes)
            {
                string nodeName = pair.Key;
                Node node = pair.Value;

                string clothingId = null;
                SlideResult? slideResult = null;
                string conditionString = null;
                int priority = 0;

                foreach (Header header in node.Headers)
                {
                    switch (header.Key)
                    {
                        case "tags":
                            foreach (string tag in header.Value.Split(' '))
                            {
                                if (tag.StartsWith("clothing:"))
                                    clothingId = tag.Substring("clothing:".Length);
                                else if (tag == "result:success")
                                    slideResult = SlideResult.Success;
                                else if (tag == "result:failure")
                                    slideResult = SlideResult.Failure;
                            }
                            break;
                        case "condition":
                            conditionString = header.Value.Trim();
                            break;
                        case "priority":
                            int.TryParse(header.Value.Trim(), out priority);
                            break;
                    }
                }

                if (clothingId == null || slideResult == null) continue;

                ClothingID clothingID = new ClothingID(clothingId);

                ICondition[] conditions = Array.Empty<ICondition>();
                if (conditionString != null)
                {
                    IEvent conditionEvent = conditionFactory.Create(conditionString);
                    if (conditionEvent != null)
                        conditions = new ICondition[] { new ConditionAdapter(conditionEvent) };
                }

                Event scenarioEvent = new Event(
                    new EventID(nodeName),
                    new ScenarioID(nodeName),
                    priority,
                    conditions);

                var key = (clothingID, slideResult.Value);
                if (!_cache.ContainsKey(key))
                    _cache[key] = new List<IScenarioEvent>();
                _cache[key].Add(scenarioEvent);
            }
        }

        public IReadOnlyList<IScenarioEvent> GetEvents(ClothingID clothingID, SlideResult result)
        {
            var key = (clothingID, result);
            return _cache.TryGetValue(key, out var events)
                ? events.AsReadOnly()
                : Array.Empty<IScenarioEvent>();
        }

        private class ConditionAdapter : ICondition
        {
            private readonly IEvent _inner;
            public ConditionAdapter(IEvent inner) { _inner = inner; }
            public bool CanFire() => _inner.CanFire();
        }
    }
}
