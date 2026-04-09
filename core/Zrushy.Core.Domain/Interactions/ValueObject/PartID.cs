// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
    public record PartID(string Value)
    {
        public override string ToString() => Value;
    }
}
