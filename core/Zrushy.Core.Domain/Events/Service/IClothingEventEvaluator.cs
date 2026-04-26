// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service
{
    public interface IClothingEventEvaluator
    {
        void Evaluate(ClothingID clothingID, SlideResult result);
    }
}
