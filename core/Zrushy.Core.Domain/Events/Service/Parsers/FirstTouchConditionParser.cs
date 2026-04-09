// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
    public class FirstTouchConditionParser : IConditionParser
    {
        private readonly IInteractionHistory _interactionHistory;

        public string Type => "first_touch";

        public FirstTouchConditionParser(IInteractionHistory interactionHistory)
        {
            _interactionHistory = interactionHistory;
        }

        public ICondition? Parse(string[] parts)
        {
            if (parts.Length != 2)
            {
                return null;
            }

            return new FirstTouchCondition(_interactionHistory, new PartID(parts[1]));
        }
    }
}
