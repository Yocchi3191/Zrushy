namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	public class Interaction
	{
		public Interaction(PartID partID, InteractionType type = InteractionType.Finger)
		{
			PartID = partID;
			Type = type;
		}

		public PartID PartID { get; internal set; }
		public InteractionType Type { get; }
	}
}