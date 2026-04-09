// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
    /// <summary>
    /// おさわりパラメータが閾値範囲内にあるときに真となる条件
    /// </summary>
    internal class ThresholdCondition : ICondition
    {
        private readonly IThresholdCheck[] _thresholds;

        public ThresholdCondition(params IThresholdCheck[] thresholds)
        {
            _thresholds = thresholds;
        }

        public bool CanFire()
        {
            foreach (IThresholdCheck threshold in _thresholds)
            {
                if (!threshold.IsInRange())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
