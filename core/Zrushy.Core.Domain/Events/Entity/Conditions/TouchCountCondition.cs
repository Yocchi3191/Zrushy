using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
	/// <summary>
	/// さわり回数が指定した回数以上のときに真となる条件
	/// </summary>
	internal class TouchCountCondition : ICondition
	{
		private readonly IInteractionHistory history;
		private readonly PartID partID;
		private readonly int minCount;

		public TouchCountCondition(IInteractionHistory history, PartID partID, int minCount)
		{
			this.history = history;
			this.partID = partID;
			this.minCount = minCount;
		}

		public bool CanFire()
		{
			return history.Get(partID).Count >= minCount;
		}
	}
}
