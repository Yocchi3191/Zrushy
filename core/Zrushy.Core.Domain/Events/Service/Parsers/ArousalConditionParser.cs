// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
    public class ArousalConditionParser : IConditionParser
    {
        private readonly IArousalReader _reader;

        public string Type => "arousal";

        public ArousalConditionParser(IArousalReader reader)
        {
            _reader = reader;
        }

        public ICondition? Parse(string[] parts)
        {
            if (parts.Length != 2)
            {
                return null;
            }

            return new ThresholdCondition(
                new Threshold<Arousal>(new Arousal(int.Parse(parts[1])), null, () => _reader.GetArousal()));
        }
    }
}
