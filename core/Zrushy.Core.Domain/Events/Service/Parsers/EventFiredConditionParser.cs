using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
	public class EventFiredConditionParser : IConditionParser
	{
		private readonly IFiredEventLog firedEventLog;

		public string Type => "event_fired";

		public EventFiredConditionParser(IFiredEventLog firedEventLog)
		{
			this.firedEventLog = firedEventLog;
		}

		public ICondition? Parse(string[] parts)
		{
			if (parts.Length != 2) return null;
			return new EventFiredCondition(firedEventLog, new EventID(parts[1]));
		}
	}
}
