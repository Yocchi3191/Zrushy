// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Events.ValueObject
{
    public record EventID(string Value)
    {
        public override string ToString() => Value;
    }
}
