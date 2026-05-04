// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation
{
    public class HeroinViewModel
    {
        public event System.Action<HeroinViewModel>? OnUpdated;
        public Dictionary<SpriteLayerID, string> SpritePaths { get; } = new();

        public void UpdateSprite(SpriteLayerID id, string newSpritePath)
        {
            SpritePaths[id] = newSpritePath;
            OnUpdated?.Invoke(this);
        }
    }
}
