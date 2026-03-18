// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation
{
    public class HeroinViewModel
    {
        public Beat? CurrentBeat { get; private set; }
        public event System.Action<HeroinViewModel>? OnUpdated;
        public Dictionary<SpriteLayerID, string> SpritePaths { get; } = new();

        public void Act(Beat beat)
        {
            CurrentBeat = beat;
            OnUpdated?.Invoke(this);
        }

        internal void UpdateSprite(SpriteLayerID id, string newSpritePath)
        {
            SpritePaths[id] = newSpritePath;
            OnUpdated?.Invoke(this);
        }
    }
}
