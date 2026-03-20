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

		public ScenarioID[] Get(EventID triggeredEvent)
		{
			var nodeName = triggeredEvent.Value;
			if (yarnProject.NodeNames.Contains(nodeName))
				return new[] { new ScenarioID(nodeName) };

			return System.Array.Empty<ScenarioID>();
		}
	}
}
