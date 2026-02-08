using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Infrastructure.Engine
{
	public class ListScenarioEngine : IScenarioEngine
	{
		public bool IsScenarioFinished { get; private set; }
		public IEvent? CurrentProceedCondition => null;
		private Scenario? currentScenario;
		private int currentIndex;

		private Scenario GetScenario(ScenarioID scenarioID)
		{
			string id = scenarioID.ToString();

			switch (id)
			{
				case "head_default":
					return new Scenario(scenarioID, new List<Action>
					{
						new Action("や、やめて…髪が…", "reaction_default", "expression_shy"),
						new Action("もう…撫でないでよ…", "reaction_default", "expression_shy"),
						new Action("…ちょっとだけなら、いいけど", "reaction_default", "expression_happy"),
					});
				case "torso_default":
					return new Scenario(scenarioID, new List<Action>
					{
						new Action("きゃっ…！", "reaction_default", "expression_shy"),
						new Action("お腹はやめて…", "reaction_default", "expression_shy"),
					});
				case "arm_default":
					return new Scenario(scenarioID, new List<Action>
					{
						new Action("腕…くすぐったい…", "reaction_default", "expression_shy"),
						new Action("そんなに触らないで…", "reaction_default", "expression_shy"),
					});
				case "hand_default":
					return new Scenario(scenarioID, new List<Action>
					{
						new Action("手…握らないで…", "reaction_default", "expression_shy"),
						new Action("…あったかい", "reaction_default", "expression_happy"),
					});
				case "waist_default":
					return new Scenario(scenarioID, new List<Action>
					{
						new Action("そこは…だめ…", "reaction_default", "expression_shy"),
						new Action("腰はだめだって…！", "reaction_default", "expression_angry"),
					});
				case "leg_default":
					return new Scenario(scenarioID, new List<Action>
					{
						new Action("脚さわらないで…", "reaction_default", "expression_shy"),
						new Action("もう…変なとこ触らないで", "reaction_default", "expression_angry"),
					});
				case "foot_default":
					return new Scenario(scenarioID, new List<Action>
					{
						new Action("足…くすぐったい！", "reaction_default", "expression_shy"),
						new Action("やめてってば！", "reaction_default", "expression_angry"),
					});
				default:
					return new Scenario(scenarioID, new List<Action>
					{
						new Action("…？", "reaction_default", "expression_neutral"),
					});
			}
		}

		public void Start(ScenarioID scenarioID)
		{
			this.currentScenario = GetScenario(scenarioID);
			this.currentIndex = 0;
			this.IsScenarioFinished = false;
		}

		public Action GetCurrentAction()
		{
			if (this.currentScenario == null)
			{
				throw new ScenarioNotStartedException();
			}

			return currentScenario[currentIndex];
		}

		public void Next()
		{
			if (this.currentScenario == null)
			{
				throw new ScenarioNotStartedException();
			}

			if (this.IsScenarioFinished)
			{
				throw new ScenarioAlreadyFinishedException();
			}

			currentIndex++;

			if (currentIndex >= currentScenario.Count)
			{
				IsScenarioFinished = true;
			}
		}
	}
}
