using System.Collections.Generic;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Entity
{
	public class Scenario
	{
		public ScenarioID ID { get; }
		private readonly IReadOnlyList<Beat> actions;

		public int Count => actions.Count;
		public Beat this[int index] => actions[index];

		public Scenario(ScenarioID id, IReadOnlyList<Beat> actions)
		{
			this.ID = id;
			this.actions = actions;
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
