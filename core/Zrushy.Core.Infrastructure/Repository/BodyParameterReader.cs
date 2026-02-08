using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
	public class BodyParameterReader : IPartParameterReader
	{
		private readonly Body body;

		public BodyParameterReader(Body body)
		{
			this.body = body;
		}

		public Pleasure GetPleasure(PartID partID) => body.GetPart(partID).Pleasure;
		public Development GetDevelopment(PartID partID) => body.GetPart(partID).Development;
		public Affection GetAffection(PartID partID) => body.GetPart(partID).Affection;
	}
}
