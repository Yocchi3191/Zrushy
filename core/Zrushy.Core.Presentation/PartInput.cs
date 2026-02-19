using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Presentation
{
	/// <summary>
	/// 部位への入力を表すクラス
	/// </summary>
	public record PartInput(PartID PartID, InteractionType Type = InteractionType.Finger);
}
