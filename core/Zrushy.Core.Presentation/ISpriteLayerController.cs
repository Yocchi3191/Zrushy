// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation
{
    public interface ISpriteLayerController
    {
        void ChangeSprite(SpriteLayerID id, LayerState state);
    }
}
