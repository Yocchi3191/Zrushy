using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
	public class BodyParameterReader : IPartParameterReader
	{
		private readonly Heroin body;

		public BodyParameterReader(Heroin body)
		{
			this.body = body;
		}

		/// <summary>
		/// 快感を取得する
		/// </summary>
		public Arousal GetArousal(PartID partID) => body.Arousal;

		public Development GetDevelopment(PartID partID) => body.GetPart(partID).Development;
		public Affection GetAffection(PartID partID) => body.GetPart(partID).Affection;
	}
}
