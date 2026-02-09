namespace Zrushy.Core.Domain.Scenarios.ValueObject
{
	public record ScenarioID(string Value)
	{
		public override string ToString() => Value;
	}
}
