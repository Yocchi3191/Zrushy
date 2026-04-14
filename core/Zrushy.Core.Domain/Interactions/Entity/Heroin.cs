// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using Zrushy.Core.Domain.Interactions.Exception;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
    /// <summary>
    /// 身体エンティティ（集約ルート）
    /// ヒロインの身体全体を管理し、各部位へのアクセスを制御する
    /// </summary>
    public class Heroin
    {
        private readonly List<IPart> _parts;

        /// <summary>
        /// Body全体の興奮度パラメータ
        /// 各部位の開発度と好感度に応じて増減する
        /// </summary>
        public Arousal Arousal { get; private set; }

        public Affection Affection { get; private set; }

        /// <summary>
        /// 絶頂判定の閾値
        /// </summary>
        private const int CLIMAX_THRESHOLD = 100;

        /// <summary>
        /// 絶頂状態かどうか
        /// </summary>
        public bool IsClimax => Arousal.IsAboveThreshold(CLIMAX_THRESHOLD);

        /// <summary>
        /// 身体を作成する
        /// </summary>
        public Heroin(Arousal arousal, Affection affection)
        {
            _parts = new List<IPart>();
            Arousal = arousal ?? new Arousal(0);
            Affection = affection ?? new Affection(0);
        }

        /// <summary>
        /// 部位を追加する
        /// </summary>
        /// <param name="part">追加する部位</param>
        public void AddPart(IPart part)
        {
            if (_parts.Any(p => p.ID == part.ID))
            {
                throw new DuplicatePartException(part.ID);
            }

            _parts.Add(part);
        }

        /// <summary>
        /// さわり操作を実行する
        /// 対象部位のパラメータを更新し、快感を蓄積し、イベント発火条件を評価する
        /// </summary>
        /// <param name="interaction">さわり操作</param>
        public void Interact(Interaction interaction)
        {
            IPart targetPart = GetPart(interaction.PartID);

            // 部位ごとの計算ロジックで興奮度を更新
            Arousal = targetPart.CalculateArousal(Arousal, interaction, Affection);

            // 部位のパラメータを更新
            targetPart.Interact(interaction);
        }

        /// <summary>
        /// 絶頂後のクールダウンを適用して快感を減少させる
        /// 全部位の平均開発度を使って減少量を補正する
        /// </summary>
        public void ApplyCooldown()
        {
            if (_parts.Count == 0)
            {
                // 部位がない場合は固定値で減少
                Arousal = Arousal.ApplyCooldown(new Development(0));
                return;
            }

            // 全部位の平均開発度を計算
            int totalDevelopment = _parts.Sum(p => p.Development.Value);
            int avgDevelopment = totalDevelopment / _parts.Count;

            // 平均開発度を使ってクールダウンを適用
            Arousal = Arousal.ApplyCooldown(new Development(avgDevelopment));
        }

        /// <summary>
        /// 指定した部位の開発度ボーナスを加算する（シナリオコマンドから呼ばれる）
        /// </summary>
        /// <param name="partID">対象部位ID</param>
        /// <param name="amount">加算量</param>
        public void ApplyDevelopmentBonus(PartID partID, int amount)
        {
            GetPart(partID).AddDevelopment(new Development(amount));
        }

        /// <summary>
        /// 指定した部位を取得する
        /// </summary>
        /// <param name="partID">部位ID</param>
        /// <returns>部位</returns>
        /// <exception cref="PartNotFoundException">部位が見つからない場合</exception>
        public IPart GetPart(PartID partID)
        {
            return _parts.Find(p => p.ID.Equals(partID))
                ?? throw new PartNotFoundException(partID);
        }
    }
}
