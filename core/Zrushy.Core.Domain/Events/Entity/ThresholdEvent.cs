using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity
{
	/// <summary>
	/// おさわりパラメータが閾値を超えたときに発火するイベント
	/// </summary>
	internal class ThresholdEvent : IEvent
	{
		public ScenarioID ScenarioToStart { get; }
		public int Priority { get; }
		private readonly IThresholdCheck[] thresholds;

		public ThresholdEvent(ScenarioID scenarioToStart, int priority, params IThresholdCheck[] thresholds)
		{
			ScenarioToStart = scenarioToStart;
			Priority = priority;
			this.thresholds = thresholds;
		}

		public bool CanFire()
		{
			foreach (var threshold in thresholds)
			{
				if (!threshold.IsInRange())
					return false;
			}
			return true;
		}
	}
}
