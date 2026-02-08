using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
	public class AffectionConditionParser : IConditionParser
	{
		private readonly IPartParameterReader parameterReader;

		public string Type => "affection";

		public AffectionConditionParser(IPartParameterReader parameterReader)
		{
			this.parameterReader = parameterReader;
		}

		public ICondition? Parse(string[] parts)
		{
			if (parts.Length != 3) return null;
			var partID = new PartID(parts[1]);
			return new ThresholdCondition(
				new Threshold<Affection>(new Affection(int.Parse(parts[2])), null, () => parameterReader.GetAffection(partID)));
		}
	}
}
