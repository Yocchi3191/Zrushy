// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

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
        private readonly PartConfig _config;

        public PartID ID { get; }
        public Development Development { get; private set; }

        public ChestPart(PartID id, Development development, PartConfig config)
        {
            ID = id;
            Development = development;
            _config = config;
        }

        /// <summary>
        /// 胸への興奮度計算
        /// 序盤は緩やかに上昇し、開発度・好感度が高まるほど大きく増加する
        /// 十分に開発されると胸イキが可能になる
        /// </summary>
        public Arousal CalculateArousal(Arousal baseArousal, Interaction interaction, Affection affection)
        {
            int gain = _config.BaseGain
                + (int)(Development.Value * _config.DevelopmentFactor)
                + (int)(affection.Value * _config.AffectionFactor);
            return baseArousal + gain;
        }

        public void Interact(Interaction interaction)
        {
            Development = Development.CalculateGain();
        }

        public void AddDevelopment(Development bonus)
        {
            Development = new Development(Development.Value + bonus.Value);
        }
    }
}
