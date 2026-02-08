using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
	/// <summary>
	/// おさわりパラメータが閾値範囲内にあるときに真となる条件
	/// </summary>
	internal class ThresholdCondition : ICondition
	{
		private readonly IThresholdCheck[] thresholds;

		public ThresholdCondition(params IThresholdCheck[] thresholds)
		{
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
