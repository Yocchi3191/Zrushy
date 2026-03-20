// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Service
{
    public class ScenarioSelector
    {
        public static ScenarioID Select(ScenarioID[] scenarios) => scenarios[0];
    }
}
