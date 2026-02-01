using Zrushy.Core.Domain.Exception;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
	/// <summary>
	/// リアクションリポジトリの実装
	/// 初期実装ではダミーデータを返す
	/// </summary>
	public class ReactionRepository : IReactionRepository
	{
		public Domain.Entity.Action GetReaction(PartID partID, Pleasure pleasure, Development development, Affection affection)
		{
			// TODO: 実際のマスターデータから取得する実装に置き換える
			// 現在は部位ごとのダミーデータを返す
			string dialogue = GetDialogue(partID);

			return new Domain.Entity.Action(
				dialogue: dialogue,
				animationName: "reaction_default",
				expressionName: "expression_shy"
			);
		}

		private string GetDialogue(PartID partID)
		{
			if (partID.Equals(new PartID("head")))
				return "や、やめて…髪が…";
			if (partID.Equals(new PartID("torso")))
				return "きゃっ…";
			if (partID.Equals(new PartID("arm")))
				return "腕…くすぐったい…";
			if (partID.Equals(new PartID("hand")))
				return "手…握らないで…";
			if (partID.Equals(new PartID("waist")))
				return "そこは…だめ…";
			if (partID.Equals(new PartID("leg")))
				return "脚さわらないで…";
			if (partID.Equals(new PartID("foot")))
				return "足…くすぐったい！";

			throw new UndefinedReactionException(partID);
		}
	}
}
