using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.ApplyBonus
{
	public record ApplyBonusCommand(PartID PartID, int Amount);
}
