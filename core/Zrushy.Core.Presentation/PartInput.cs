using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Presentation
{
	/// <summary>
	/// 部位への入力を表すクラス
	/// </summary>
	public class PartInput
	{
		/// <summary>
		/// さわった部位のID
		/// </summary>
		public PartID PartID { get; }

		public PartInput(PartID partID)
		{
			PartID = partID;
		}
	}
}
