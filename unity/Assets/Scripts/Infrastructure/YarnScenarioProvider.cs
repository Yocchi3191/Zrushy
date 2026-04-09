// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Linq;
using Google.Protobuf.Collections;
using Yarn;
using Yarn.Unity;
using Zrushy.Core.Application.UseCase.GetScenario;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Infrastructure.Unity
{
    /// <summary>
    /// yarnProjectのノード名をシナリオIDとして返すプロバイダ
    /// </summary>
    public class YarnScenarioProvider : IScenarioProvider
    {
        private readonly YarnProject _yarnProject;

        public YarnScenarioProvider(YarnProject yarnProject)
        {
            _yarnProject = yarnProject;
        }

        public ScenarioInfo[] GetPlayableScenarios(EventID triggeredEvent)
        {
            string nodeName = triggeredEvent.Value;
            if (!_yarnProject.NodeNames.Contains(nodeName))
                return System.Array.Empty<ScenarioInfo>();

            RepeatedField<Header> headers = _yarnProject.Program.Nodes[nodeName].Headers;

            Header priorityHeader = headers.FirstOrDefault(h => h.Key == "priority");
            int priority = priorityHeader != null && int.TryParse(priorityHeader.Value, out var parsedPriority) ? parsedPriority : 0;

            return new[] { new ScenarioInfo(new ScenarioID(nodeName), new Priority(priority)) };
        }
    }
}
