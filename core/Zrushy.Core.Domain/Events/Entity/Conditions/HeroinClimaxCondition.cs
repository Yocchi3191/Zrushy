using System;
using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
	internal class HeroinClimaxCondition : ICondition
	{
		private readonly Func<Heroin> heroinFactory;

		public HeroinClimaxCondition(Func<Heroin> heroinFactory)
		{
			this.heroinFactory = heroinFactory;
		}

		public bool CanFire() => heroinFactory().IsClimax;
	}
}
