// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.Service;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Application.UseCase.GetScenario
{
    public class GetScenario
    {
        private readonly IScenarioProvider provider;
        public GetScenario(IScenarioProvider provider)
        {
            this.provider = provider;
        }

        public ScenarioID Execute(EventID triggeredEvent)
        {
            ScenarioID[] scenarioIDs = provider.Get(triggeredEvent);
            return ScenarioSelector.Select(scenarioIDs);
        }
    }
}
