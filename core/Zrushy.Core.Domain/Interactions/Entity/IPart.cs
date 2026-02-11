using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
	/// <summary>
	/// 部位インターフェース
	/// 部位といっても腕、胸、秘部などでそれぞれ快感のたまり方が異なる
	/// </summary>
	public interface IPart
	{
		PartID ID { get; }
		Development Development { get; }
		Affection Affection { get; }

		/// <summary>
		/// 興奮度計算
		/// 状態によっては不快になる部位もある
		/// </summary>
		Arousal CalculateArousal(Arousal baseArousal);

		/// <summary>
		/// 覚醒度計算
		/// 部位ごと、開発度等の要素によって上昇量が変化する
		/// </summary>
		Awakeness CalculateAwakeness(Awakeness baseAwakeness);

		/// <summary>
		/// さわり操作による内部状態の更新
		/// </summary>
		void Interact(Interaction interaction);
	}
}
