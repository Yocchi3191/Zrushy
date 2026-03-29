using System.Linq;
using Yarn.Unity;
using Zrushy.Core.Application.UseCase.GetScenario;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Infrastructure
{
	/// <summary>
	/// yarnProjectのノード名をシナリオIDとして返すプロバイダ
	/// </summary>
	public class YarnScenarioProvider : IScenarioProvider
	{
		private readonly YarnProject yarnProject;

		public YarnScenarioProvider(YarnProject yarnProject)
		{
			this.yarnProject = yarnProject;
		}

		public ScenarioInfo[] GetPlayableScenarios(EventID triggeredEvent)
		{
			var nodeName = triggeredEvent.Value;
			if (!yarnProject.NodeNames.Contains(nodeName))
				return System.Array.Empty<ScenarioInfo>();

			var headers = yarnProject.Program.Nodes[nodeName].Headers;

			var priorityHeader = headers.FirstOrDefault(h => h.Key == "priority");
			var priority = priorityHeader != null && int.TryParse(priorityHeader.Value, out var parsedPriority) ? parsedPriority : 0;

			if (yarnProject.NodeNames.Contains(nodeName))
				return new[] { new ScenarioInfo(new ScenarioID(nodeName), new Priority(priority)) };
			return System.Array.Empty<ScenarioInfo>();
		}
	}
}
