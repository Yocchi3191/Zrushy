// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

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
        private readonly T? _min;
        private readonly T? _max;
        private readonly Func<T> _getValue;

        public Threshold(T? min, T? max, Func<T> getValue)
        {
            _min = min;
            _max = max;
            _getValue = getValue;
        }

        public bool IsInRange()
        {
            T value = _getValue();
            if (_min != null && value.CompareTo(_min) < 0)
            {
                return false;
            }

            if (_max != null && value.CompareTo(_max) > 0)
            {
                return false;
            }

            return true;
        }
    }
}
