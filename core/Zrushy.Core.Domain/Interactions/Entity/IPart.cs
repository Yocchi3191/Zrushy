// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

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
        Arousal CalculateArousal(Arousal baseArousal, Interaction interaction);

        /// <summary>
        /// さわり操作による内部状態の更新
        /// </summary>
        void Interact(Interaction interaction);

        /// <summary>
        /// 開発度ボーナスを加算する（シナリオコマンドから呼ばれる）
        /// </summary>
        void AddDevelopment(Development bonus);
    }
}
