// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;

namespace Zrushy.Core.Domain.Scenarios.ValueObject
{
    public record Priority : IComparable<Priority>
    {
        public int Value { get; init; }

        public Priority(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            Value = value;
        }

        public int CompareTo(Priority other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Value.CompareTo(other.Value);
        }
    }
}
