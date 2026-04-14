// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
    /// <summary>
    /// 部位エンティティ
    /// ヒロインの身体の各部位を表現する
    /// </summary>
    public class Part : IPart
    {
        private readonly PartConfig _config;

        public PartID ID { get; }
        public Development Development { get; private set; }
        public Part(PartID id, Development development, PartConfig config)
        {
            ID = id;
            Development = development;
            _config = config;
        }

        public void Interact(Interaction interaction)
        {
            Development = Development.CalculateGain();
        }

        public Arousal CalculateArousal(Arousal baseArousal, Interaction interaction, Affection affection)
        {
            int developmentBonus = (int)(Development.Value * _config.DevelopmentFactor);
            int affectionBonus = (int)(affection.Value * _config.AffectionFactor);
            int totalGain = _config.BaseGain + developmentBonus + affectionBonus;
            return baseArousal + totalGain;
        }

        public void AddDevelopment(Development bonus)
        {
            Development = new Development(Development.Value + bonus.Value);
        }

    }
}
