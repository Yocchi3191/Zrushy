// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Presentation.Unity
{
    public class ClothingInitializer : MonoBehaviour
    {
        [Inject] private readonly Heroin _heroin;
        [SerializeField] private ClothingConfig[] _clothingConfigs;
        private
        void Start()
        {
            foreach (ClothingConfig config in _clothingConfigs)
            {
                _heroin.AddClothing(new Clothing(
                    new ClothingID(config.ID),
                    config.Resistance
                ));
            }
        }

        [System.Serializable]
        private class ClothingConfig
        {
            public string ID;
            [Min(0)] public int Resistance; // 服脱がしに対する抵抗値
        }
    }
}
