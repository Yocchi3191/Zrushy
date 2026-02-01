using System.Collections.Generic;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Infrastructure.Engine
{
	public class ListScenarioEngine : IScenarioEngine
	{
		private List<Action> actions = new List<Action>();
		private int currentIndex;

		public bool IsScenarioFinished => currentIndex >= actions.Count - 1;

		public void Start(ScenarioID scenarioID)
		{
			actions = GetActions(scenarioID);
			currentIndex = 0;
		}

		public Action GetCurrentAction()
		{
			return actions[currentIndex];
		}

		public void Next()
		{
			if (!IsScenarioFinished)
			{
				currentIndex++;
			}
		}

		private List<Action> GetActions(ScenarioID scenarioID)
		{
			string id = scenarioID.ToString();

			switch (id)
			{
				case "head_default":
					return new List<Action>
					{
						new Action("や、やめて…髪が…", "reaction_default", "expression_shy"),
						new Action("もう…撫でないでよ…", "reaction_default", "expression_shy"),
						new Action("…ちょっとだけなら、いいけど", "reaction_default", "expression_happy"),
					};
				case "torso_default":
					return new List<Action>
					{
						new Action("きゃっ…！", "reaction_default", "expression_shy"),
						new Action("お腹はやめて…", "reaction_default", "expression_shy"),
					};
				case "arm_default":
					return new List<Action>
					{
						new Action("腕…くすぐったい…", "reaction_default", "expression_shy"),
						new Action("そんなに触らないで…", "reaction_default", "expression_shy"),
					};
				case "hand_default":
					return new List<Action>
					{
						new Action("手…握らないで…", "reaction_default", "expression_shy"),
						new Action("…あったかい", "reaction_default", "expression_happy"),
					};
				case "waist_default":
					return new List<Action>
					{
						new Action("そこは…だめ…", "reaction_default", "expression_shy"),
						new Action("腰はだめだって…！", "reaction_default", "expression_angry"),
					};
				case "leg_default":
					return new List<Action>
					{
						new Action("脚さわらないで…", "reaction_default", "expression_shy"),
						new Action("もう…変なとこ触らないで", "reaction_default", "expression_angry"),
					};
				case "foot_default":
					return new List<Action>
					{
						new Action("足…くすぐったい！", "reaction_default", "expression_shy"),
						new Action("やめてってば！", "reaction_default", "expression_angry"),
					};
				default:
					return new List<Action>
					{
						new Action("…？", "reaction_default", "expression_neutral"),
					};
			}
		}
	}
}
