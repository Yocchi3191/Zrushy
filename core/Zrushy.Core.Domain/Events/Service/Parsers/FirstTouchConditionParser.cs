using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
	public class FirstTouchConditionParser : IConditionParser
	{
		private readonly IInteractionHistory interactionHistory;

		public string Type => "first_touch";

		public FirstTouchConditionParser(IInteractionHistory interactionHistory)
		{
			this.interactionHistory = interactionHistory;
		}

		public ICondition? Parse(string[] parts)
		{
			if (parts.Length != 2) return null;
			return new FirstTouchCondition(interactionHistory, new PartID(parts[1]));
		}
	}
}
