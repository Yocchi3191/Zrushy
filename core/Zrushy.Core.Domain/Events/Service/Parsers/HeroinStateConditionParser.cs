using System;
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
		private readonly Func<Heroin> heroinFactory;

		public string Type => "heroin";

		public HeroinStateConditionParser(Func<Heroin> heroinFactory)
		{
			this.heroinFactory = heroinFactory;
		}

		public ICondition? Parse(string[] parts)
		{
			if (parts.Length != 2 || parts[1] != "climax") return null;
			return new HeroinClimaxCondition(heroinFactory);
		}
	}
}
