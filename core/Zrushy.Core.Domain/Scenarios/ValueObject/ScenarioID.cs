namespace Zrushy.Core.Domain.Scenarios.ValueObject
{
	public class ScenarioID
	{
		private readonly string value;

		public ScenarioID(string value)
		{
			this.value = value;
		}

		public override bool Equals(object? obj)
		{
			if (obj is ScenarioID other)
			{
				return value == other.value;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public override string ToString()
		{
			return value;
		}
	}
}
