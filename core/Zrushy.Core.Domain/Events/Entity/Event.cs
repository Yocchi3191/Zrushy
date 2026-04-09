// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity
{

    public class Event : IScenarioEvent
    {
        public EventID ID { get; }
        public ScenarioID ScenarioToStart { get; }
        public int Priority { get; }

        private readonly ICondition[] _conditions;

        public Event(EventID id, ScenarioID scenarioToStart, int priority, params ICondition[] conditions)
        {
            ID = id;
            ScenarioToStart = scenarioToStart;
            Priority = priority;
            _conditions = conditions;
        }

        public bool CanFire()
        {
            foreach (ICondition condition in _conditions)
            {
                if (!condition.CanFire())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
