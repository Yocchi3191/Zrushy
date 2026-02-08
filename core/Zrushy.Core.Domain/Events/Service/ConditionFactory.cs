using System;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service
{
	/// <summary>
	/// 条件文字列からIEventを生成するファクトリ
	/// 書式: "type:param1:param2,type2:param1:param2"
	/// 単条件はIConditionを返す。複数条件はEventに包んで返す。
	/// </summary>
	public class ConditionFactory : IConditionFactory
	{
		private readonly IInteractionHistory interactionHistory;
		private readonly IFiredEventLog firedEventLog;
		private readonly IPartParameterReader parameterReader;

		public ConditionFactory(
			IInteractionHistory interactionHistory,
			IFiredEventLog firedEventLog,
			IPartParameterReader parameterReader)
		{
			this.interactionHistory = interactionHistory;
			this.firedEventLog = firedEventLog;
			this.parameterReader = parameterReader;
		}

		public IEvent? Create(string conditionString)
		{
			if (string.IsNullOrWhiteSpace(conditionString))
				return null;

			var parts = conditionString.Split(',');
			if (parts.Length == 1)
				return CreateSingle(parts[0].Trim());

			var conditions = new ICondition[parts.Length];
			for (int i = 0; i < parts.Length; i++)
			{
				var condition = CreateSingle(parts[i].Trim());
				if (condition == null)
					return null;
				conditions[i] = condition;
			}

			return new Event(new EventID("__composite__"), new Scenarios.ValueObject.ScenarioID(""), 0, conditions);
		}

		private ICondition? CreateSingle(string token)
		{
			var parts = token.Split(':');
			if (parts.Length == 0)
				return null;

			string type = parts[0];

			switch (type)
			{
				case "touch_count" when parts.Length == 3:
					return new TouchCountCondition(
						interactionHistory,
						new PartID(parts[1]),
						int.Parse(parts[2]));

				case "first_touch" when parts.Length == 2:
					return new FirstTouchCondition(
						interactionHistory,
						new PartID(parts[1]));

				case "event_fired" when parts.Length == 2:
					return new EventFiredCondition(
						firedEventLog,
						new EventID(parts[1]));

				case "pleasure" when parts.Length == 3:
					var partID_p = new PartID(parts[1]);
					return new ThresholdCondition(
						new Threshold<Pleasure>(new Pleasure(int.Parse(parts[2])), null, () => parameterReader.GetPleasure(partID_p)));

				case "development" when parts.Length == 3:
					var partID_d = new PartID(parts[1]);
					return new ThresholdCondition(
						new Threshold<Development>(new Development(int.Parse(parts[2])), null, () => parameterReader.GetDevelopment(partID_d)));

				case "affection" when parts.Length == 3:
					var partID_a = new PartID(parts[1]);
					return new ThresholdCondition(
						new Threshold<Affection>(new Affection(int.Parse(parts[2])), null, () => parameterReader.GetAffection(partID_a)));

				default:
					return null;
			}
		}
	}
}
