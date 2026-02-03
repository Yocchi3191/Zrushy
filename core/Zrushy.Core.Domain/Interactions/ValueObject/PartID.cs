namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	public class PartID
	{
		private string v;

		public PartID(string v)
		{
			this.v = v;
		}

		public override bool Equals(object? obj)
		{
			if (obj is PartID other)
			{
				return v == other.v;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return v.GetHashCode();
		}

		public override string ToString()
		{
			return v;
		}
	}
}