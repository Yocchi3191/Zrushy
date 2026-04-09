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
    public class YarnEventRepository : IEventRepository
    {
        private readonly Dictionary<PartID, List<IScenarioEvent>> _cache = new();

        public YarnEventRepository(DialogueRunner dialogueRunner, IConditionFactory conditionFactory)
        {
            foreach (var pair in dialogueRunner.YarnProject.Program.Nodes)
            {
                string nodeName = pair.Key;
                Node node = pair.Value;

                string partId = null;
                string conditionString = null;
                int priority = 0;

                foreach (Header header in node.Headers)
                {
                    switch (header.Key)
                    {
                        case "tags":
                            foreach (string tag in header.Value.Split(' '))
                            {
                                if (tag.StartsWith("part:"))
                                {
                                    partId = tag.Substring("part:".Length);
                                    break;
                                }
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

                if (partId == null) continue;

                PartID partID = new PartID(partId);

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

                if (!_cache.ContainsKey(partID))
                    _cache[partID] = new List<IScenarioEvent>();
                _cache[partID].Add(scenarioEvent);
            }
        }

        private static readonly PartID s_globalPartID = new PartID("public");

        public IReadOnlyList<IScenarioEvent> GetEvents(PartID partID)
        {
            return _cache.TryGetValue(partID, out var events)
                ? events.AsReadOnly()
                : Array.Empty<IScenarioEvent>();
        }

        public IReadOnlyList<IScenarioEvent> GetGlobalEvents()
        {
            return _cache.TryGetValue(s_globalPartID, out var events)
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
