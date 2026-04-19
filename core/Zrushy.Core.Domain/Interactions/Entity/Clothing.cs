// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
    public class Clothing : IClothing
    {
        public ClothingID ID { get; init; }
        private readonly int _resistance;

        public Clothing(ClothingID id, int resistance)
        {

            ID = id ?? throw new ArgumentNullException(nameof(id));

            if (resistance < 0)
                throw new ArgumentOutOfRangeException(nameof(resistance));

            _resistance = resistance;
        }

        public bool CanPutOff(Affection affection, Arousal arousal) => affection.Value + arousal.Value >= _resistance;
    }
}
