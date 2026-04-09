// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Sprite
{
    public interface ISpriteLayerRepository
    {
        string Get(SpriteLayerID id, LayerState state);
    }
}
