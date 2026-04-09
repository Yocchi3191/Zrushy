// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Repository
{
    [Serializable]
    public class ScenarioNotFoundException : System.Exception
    {
        public ScenarioID? ScenarioID { get; }

        public ScenarioNotFoundException(string message) : base(message)
        {
        }

        public ScenarioNotFoundException(ScenarioID scenarioID)
            : base($"Scenario '{scenarioID.Value}' not found. Please create a corresponding .yarn file.")
        {
            ScenarioID = scenarioID;
        }
    }
}
