// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
    public class ZrushyHistory : IZrushyHistory
    {
        private readonly List<(ClothingID clothingID, SlideResult result)> _records = new();

        public void Record(ClothingID clothingID, SlideResult result)
        {
            _records.Add((clothingID, result));
        }
    }
}
