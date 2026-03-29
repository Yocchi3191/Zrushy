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
        private readonly ScenarioSelector selector;

        public GetScenario(IScenarioProvider provider, ScenarioSelector selector)
        {
            this.provider = provider;
            this.selector = selector;
        }

        public ScenarioInfo Execute(EventID triggeredEvent)
        {
            ScenarioInfo[] scenarios = provider.GetPlayableScenarios(triggeredEvent);
            return selector.SelectScenarioInfoToPlay(scenarios);
        }
    }
}
