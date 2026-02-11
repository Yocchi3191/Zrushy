using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
	/// <summary>
	/// 胸部位エンティティ
	/// 序盤から快感を稼ぎやすく、開発度・好感度が十分に高まると胸イキも可能
	/// SecretPartの濡れ状態を引き出す前戯として序盤から機能する
	/// </summary>
	public class ChestPart : IPart
	{
		/// <summary>
		/// 開発度・好感度によらず序盤から得られる基本ゲイン
		/// 初回から快感を感じやすい部位
		/// </summary>
		private const int BASE_GAIN = 3;

		public PartID ID { get; }
		public Development Development { get; private set; }
		public Affection Affection { get; private set; }

		public ChestPart(PartID id, Development development, Affection affection)
		{
			ID = id;
			Development = development;
			Affection = affection;
		}

		/// <summary>
		/// 胸への興奮度計算
		/// 序盤は緩やかに上昇し、開発度・好感度が高まるほど大きく増加する
		/// 十分に開発されると胸イキが可能になる
		/// </summary>
		public Arousal CalculateArousal(Arousal baseArousal, Interaction interaction)
		{
			// 計算式: 基本値3 + (開発度 * 0.1) + (好感度 * 0.05)
			// dev=0, aff=0 → gain=3  (序盤から小さく稼ぐ)
			// dev=100, aff=100 → gain=18 (十分な開発で絶頂も可能)
			int gain = BASE_GAIN + (int)(Development.Value * 0.1) + (int)(Affection.Value * 0.05);
			return baseArousal + gain;
		}

		public Awakeness CalculateAwakeness(Awakeness baseAwakeness)
		{
			throw new System.NotImplementedException();
		}

		public void Interact(Interaction interaction)
		{
			Development = Development.CalculateGain();
			Affection = Affection.CalculateGain();
		}
	}
}
