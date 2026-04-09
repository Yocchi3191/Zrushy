// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity
{
    public interface IScenarioEvent : IEvent
    {
        EventID ID { get; }
        ScenarioID ScenarioToStart { get; }
        int Priority { get; }
    }
}
