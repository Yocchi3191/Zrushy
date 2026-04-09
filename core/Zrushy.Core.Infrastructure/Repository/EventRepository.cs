// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
    public class EventRepository : IEventRepository
    {
        public IReadOnlyList<IScenarioEvent> GetEvents(PartID partID) => Array.Empty<IScenarioEvent>();

        public IReadOnlyList<IScenarioEvent> GetGlobalEvents() => Array.Empty<IScenarioEvent>();
    }
}
