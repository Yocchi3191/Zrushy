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
        private readonly IAffectionReader _reader;

        public string Type => "affection";

        public AffectionConditionParser(IAffectionReader reader)
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
                new Threshold<Affection>(new Affection(int.Parse(parts[1])), null, () => _reader.GetAffection()));
        }
    }
}
