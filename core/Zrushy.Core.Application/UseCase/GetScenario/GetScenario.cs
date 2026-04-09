// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.Service;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Application.UseCase.GetScenario
{
    public class GetScenario
    {
        private readonly IScenarioProvider _provider;
        private readonly ScenarioSelector _selector;

        public GetScenario(IScenarioProvider provider, ScenarioSelector selector)
        {
            _provider = provider;
            _selector = selector;
        }

        public ScenarioInfo Execute(EventID triggeredEvent)
        {
            ScenarioInfo[] scenarios = _provider.GetPlayableScenarios(triggeredEvent);
            return _selector.SelectScenarioInfoToPlay(scenarios);
        }
    }
}
