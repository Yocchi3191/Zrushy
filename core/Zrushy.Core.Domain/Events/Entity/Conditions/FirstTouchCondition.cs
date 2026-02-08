using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
	/// <summary>
	/// 対応する部位への初回おさわり時に真となる条件
	/// </summary>
	internal class FirstTouchCondition : ICondition
	{
		private readonly IInteractionHistory history;
		private readonly PartID partID;

		public FirstTouchCondition(IInteractionHistory history, PartID partID)
		{
			this.history = history;
			this.partID = partID;
		}

		public bool CanFire()
		{
			return history.Get(partID).Count == 0;
		}
	}
}
