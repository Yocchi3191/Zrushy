namespace Zrushy.Core.Domain.Events.ValueObject
{
	public record EventID(string Value)
	{
		public override string ToString() => Value;
	}
}
