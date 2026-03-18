// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Zrushy.Core.Domain.Sprite
{
    public interface ISpriteLayerRepository
    {
        string Get(SpriteLayerID id, LayerState state);
    }
}
