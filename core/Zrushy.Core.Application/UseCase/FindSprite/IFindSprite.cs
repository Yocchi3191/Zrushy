// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Application.UseCase.FindSprite
{
    public interface IFindSprite
    {
        public string Execute(SpriteLayerID id, LayerState state);
    }
}
