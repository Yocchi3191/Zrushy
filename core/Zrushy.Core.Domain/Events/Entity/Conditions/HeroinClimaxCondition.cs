using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
	internal class HeroinClimaxCondition : ICondition
	{
		private readonly Heroin heroin;

		public HeroinClimaxCondition(Heroin heroin)
		{
			this.heroin = heroin;
		}

		public bool CanFire() => heroin.IsClimax;
	}
}
