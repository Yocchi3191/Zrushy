// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
    public class TouchCountConditionParser : IConditionParser
    {
        private readonly IInteractionHistory _interactionHistory;

        public string Type => "touch_count";

        public TouchCountConditionParser(IInteractionHistory interactionHistory)
        {
            _interactionHistory = interactionHistory;
        }

        public ICondition? Parse(string[] parts)
        {
            if (parts.Length != 3)
            {
                return null;
            }

            return new TouchCountCondition(_interactionHistory, new PartID(parts[1]), int.Parse(parts[2]));
        }
    }
}
