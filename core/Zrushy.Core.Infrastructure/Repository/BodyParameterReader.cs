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
		/// 注意: 快感はBody全体で管理されているため、PartIDは無視される
		/// </summary>
		public Arousal GetPleasure(PartID partID) => body.Arousal;

		public Development GetDevelopment(PartID partID) => body.GetPart(partID).Development;
		public Affection GetAffection(PartID partID) => body.GetPart(partID).Affection;
	}
}
