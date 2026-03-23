using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Domain.Events.Service.Parsers
{
	/// <summary>
	/// Heroinの状態に基づく条件パーサー
	/// 書式: heroin:climax
	/// </summary>
	public class HeroinStateConditionParser : IConditionParser
	{
		private readonly Heroin heroin;

		public string Type => "heroin";

		public HeroinStateConditionParser(Heroin heroin)
		{
			this.heroin = heroin;
		}

		public ICondition? Parse(string[] parts)
		{
			if (parts.Length != 2 || parts[1] != "climax") return null;
			return new HeroinClimaxCondition(heroin);
		}
	}
}
