// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
    public class AffectionConditionParser : IConditionParser
    {
        private readonly IPartParameterReader _parameterReader;

        public string Type => "affection";

        public AffectionConditionParser(IPartParameterReader parameterReader)
        {
            _parameterReader = parameterReader;
        }

        public ICondition? Parse(string[] parts)
        {
            if (parts.Length != 3)
            {
                return null;
            }

            PartID partID = new PartID(parts[1]);
            return new ThresholdCondition(
                new Threshold<Affection>(new Affection(int.Parse(parts[2])), null, () => _parameterReader.GetAffection(partID)));
        }
    }
}
