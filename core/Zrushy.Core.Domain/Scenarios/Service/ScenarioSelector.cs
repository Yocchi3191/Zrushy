// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Service
{
    public class ScenarioSelector
    {
        public static ScenarioID Select(ScenarioID[] scenarios)
        {
            if (scenarios == null || scenarios.Length == 0)
            {
                throw new ScenarioNotFoundException("Scenarios array cannot be null or empty.");
            }

            return scenarios[0];
        }
    }
}
