// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Entity
{
    public interface IClothing
    {
        ClothingID ID { get; }
        bool CanPutOff(Affection affection, Arousal arousal);
    }
}
