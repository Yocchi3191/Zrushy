namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	public class Interaction
	{
		public Interaction(PartID partID)
		{
			PartID = partID;
		}

		public PartID PartID { get; internal set; }
	}
}