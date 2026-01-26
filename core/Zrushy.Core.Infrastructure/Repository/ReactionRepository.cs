using System;
using Zrushy.Core.Domain.Entity;
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
		public Reaction GetReaction(PartID partID, Pleasure pleasure, Development development, Affection affection)
		{
			// TODO: 実際のマスターデータから取得する実装に置き換える
			// 現在はダミーデータを返す
			return new Reaction(
				dialogue: "あっ…",
				animationName: "reaction_default",
				expressionName: "expression_shy",
				voiceClipName: "voice_reaction_01"
			);
		}
	}
}
