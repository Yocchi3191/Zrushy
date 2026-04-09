// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
    /// <summary>
    /// 秘部エンティティ
    /// 濡れ状態と処女状態によって興奮度の変化が大きく異なる
    /// </summary>
    public class SecretPart : IPart
    {
        private readonly SecretPartConfig _config;
        private bool _virginityIntact;

        public PartID ID { get; }
        public Development Development { get; private set; }
        public Affection Affection { get; private set; }

        /// <param name="virginityIntact">処女かどうか</param>
        public SecretPart(PartID id, Development development, Affection affection, SecretPartConfig config, bool virginityIntact = true)
        {
            ID = id;
            Development = development;
            Affection = affection;
            _config = config;
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
                return baseArousal - _config.VirginityLossPenalty;
            }

            bool isWet = baseArousal.IsAboveThreshold(_config.WetThreshold);

            // 乾燥状態: 高い不快感
            if (!isWet)
            {
                return baseArousal - _config.DryDiscomfort;
            }

            // 濡れ状態: 開発度に強く依存したゲイン
            return interaction.Type == InteractionType.Penis
                ? baseArousal + (_config.PenisWetBase + (int)(Development.Value * _config.PenisWetDevFactor))
                : baseArousal + (_config.FingerWetBase + (int)(Development.Value * _config.FingerWetDevFactor));
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

        public void AddDevelopment(Development bonus)
        {
            Development = new Development(Development.Value + bonus.Value);
        }
    }
}
