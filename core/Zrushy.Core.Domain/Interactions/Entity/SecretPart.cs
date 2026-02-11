using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
	/// <summary>
	/// 秘部エンティティ
	/// 濡れ状態と処女状態によって興奮度の変化が大きく異なる
	/// </summary>
	public class SecretPart : IPart
	{
		/// <summary>
		/// 濡れ状態に転じるArousalの閾値（全体Arousalの約40%）
		/// </summary>
		private const int WET_THRESHOLD = 40;

		/// <summary>
		/// 乾燥状態での不快感（高め）
		/// </summary>
		private const int DRY_DISCOMFORT = 12;

		/// <summary>
		/// 処女喪失前のペニス挿入による大幅な興奮度減少
		/// 就寝中に行うとAwakeness上昇リスクが非常に高くなる
		/// </summary>
		private const int VIRGINITY_LOSS_PENALTY = 60;

		private bool _virginityIntact;

		public PartID ID { get; }
		public Development Development { get; private set; }
		public Affection Affection { get; private set; }

		public SecretPart(PartID id, Development development, Affection affection, bool virginityIntact = true)
		{
			ID = id;
			Development = development;
			Affection = affection;
			_virginityIntact = virginityIntact;
		}

		/// <summary>
		/// 秘部への興奮度計算
		/// 濡れ状態・処女状態・さわり方・開発度によって大きく変わる
		/// </summary>
		public Arousal CalculateArousal(Arousal baseArousal, Interaction interaction)
		{
			// 処女喪失前のペニス挿入: 強い痛みで大幅に減少
			if (interaction.Type == InteractionType.Penis && _virginityIntact)
			{
				return baseArousal - VIRGINITY_LOSS_PENALTY;
			}

			bool isWet = baseArousal.IsAboveThreshold(WET_THRESHOLD);

			// 乾燥状態: 高い不快感
			if (!isWet)
			{
				return baseArousal - DRY_DISCOMFORT;
			}

			// 濡れ状態: 開発度に強く依存したゲイン
			// 開発度が低いうちは「濡れているがまだ慣れていない」状態
			return interaction.Type == InteractionType.Penis
				? baseArousal + (2 + (int)(Development.Value * 0.2))   // ペニス: 2〜22
				: baseArousal + (1 + (int)(Development.Value * 0.1));  // 指: 1〜11
		}

		public Awakeness CalculateAwakeness(Awakeness baseAwakeness)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// さわり操作による内部状態の更新
		/// ペニス挿入かつ処女の場合、処女喪失を記録する
		/// </summary>
		public void Interact(Interaction interaction)
		{
			if (interaction.Type == InteractionType.Penis && _virginityIntact)
			{
				_virginityIntact = false;
			}

			Development = Development.CalculateGain();
			Affection = Affection.CalculateGain();
		}
	}
}
