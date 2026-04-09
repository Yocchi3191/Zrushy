// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
    public class EventFiredConditionParser : IConditionParser
    {
        private readonly IFiredEventLog _firedEventLog;

        public string Type => "event_fired";

        public EventFiredConditionParser(IFiredEventLog firedEventLog)
        {
            _firedEventLog = firedEventLog;
        }

        public ICondition? Parse(string[] parts)
        {
            if (parts.Length != 2)
            {
                return null;
            }

            return new EventFiredCondition(_firedEventLog, new EventID(parts[1]));
        }
    }
}
