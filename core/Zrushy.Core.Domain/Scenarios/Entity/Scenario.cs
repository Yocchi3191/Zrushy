using System.Collections.Generic;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Entity
{
	public class Scenario
	{
		public ScenarioID ID { get; }
		private readonly IReadOnlyList<Action> actions;
		private int currentIndex;

		public bool IsScenarioFinished
		{
			get
			{
				return currentIndex >= actions.Count || actions.Count < 1;
			}
		}

		public Scenario(ScenarioID id, IEnumerable<Action> actions)
		{
			this.ID = id;
			this.actions = (IReadOnlyList<Action>)actions;
			currentIndex = -1;
		}

		public Action First()
		{
			currentIndex = 0;
			Action result = actions[currentIndex] ?? throw new ActionNotFoundException();
			return result;
		}

		public Action Next()
		{
			if (currentIndex < 0)
			{
				throw new ScenarioNotStartedException();
			}

			currentIndex++;
			Action result = actions[currentIndex] ?? throw new ActionNotFoundException();
			return result;
		}

		public override bool Equals(object obj)
		{
			Scenario target = (Scenario)obj;
			return target.ID == ID;
		}

		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}