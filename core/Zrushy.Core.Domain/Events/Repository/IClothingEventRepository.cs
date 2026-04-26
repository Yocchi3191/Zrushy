// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Repository
{
    public interface IClothingEventRepository
    {
        IReadOnlyList<IScenarioEvent> GetEvents(ClothingID clothingID, SlideResult result);
    }
}
