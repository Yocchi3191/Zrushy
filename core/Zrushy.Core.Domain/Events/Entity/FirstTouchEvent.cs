using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity
{
	/// <summary>
	/// 対応する部位への初回おさわり時に発火するイベント
	/// </summary>
	internal class FirstTouchEvent : IEvent
	{
		public ScenarioID ScenarioToStart { get; }
		public int Priority { get; }
		private IInteractionHistory history { get; }
		private PartID PartID { get; }

		/// <summary>
		/// 1度も触られていない場合にtrueを返す
		/// </summary>
		/// <returns></returns>
		public bool CanFire()
		{
			int interactionCount = history.Get(PartID).Count;
			if (interactionCount == 0)
			{
				return true;
			}

			return false;
		}

		public FirstTouchEvent(ScenarioID scenarioToStart, int priority, IInteractionHistory history, PartID partID)
		{
			ScenarioToStart = scenarioToStart;
			Priority = priority;
			this.history = history;
			PartID = partID;
		}
	}
}
