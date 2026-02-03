using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Service
{
	public interface IPartParameterReader
	{
		Pleasure GetPleasure(PartID partID);
		Development GetDevelopment(PartID partID);
		Affection GetAffection(PartID partID);
	}
}
