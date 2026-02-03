namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	public record PartID(string Value)
	{
		public override string ToString() => Value;
	}
}
