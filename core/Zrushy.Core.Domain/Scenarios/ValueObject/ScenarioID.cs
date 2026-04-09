// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Scenarios.ValueObject
{
    public record ScenarioID(string Value)
    {
        public override string ToString() => Value;
    }
}
