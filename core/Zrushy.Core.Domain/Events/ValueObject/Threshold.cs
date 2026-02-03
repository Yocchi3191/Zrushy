using System;

namespace Zrushy.Core.Domain.Events.ValueObject
{
	internal interface IThresholdCheck
	{
		bool IsInRange();
	}

	/// <summary>
	/// min, maxの範囲内に値があるかをチェックする
	/// </summary>
	internal class Threshold<T> : IThresholdCheck where T : class, IComparable<T>
	{
		private readonly T? min;
		private readonly T? max;
		private readonly Func<T> getValue;

		public Threshold(T? min, T? max, Func<T> getValue)
		{
			this.min = min;
			this.max = max;
			this.getValue = getValue;
		}

		public bool IsInRange()
		{
			var value = getValue();
			if (min != null && value.CompareTo(min) < 0)
				return false;
			if (max != null && value.CompareTo(max) > 0)
				return false;
			return true;
		}
	}
}
