namespace Zrushy.Core.Domain.Interactions.ValueObject
{
	public record Interaction(PartID PartID, InteractionType Type = InteractionType.Finger);
}
