// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Application.UseCase.ChangeSprite;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation
{
    public class SpriteLayerController
    {
        private readonly ChangeSprite _changeSprite;
        private readonly HeroinViewModel _heroinViewModel;
        public SpriteLayerController(ChangeSprite changeSprite, HeroinViewModel heroinViewModel)
        {
            _changeSprite = changeSprite;
            _heroinViewModel = heroinViewModel;
        }
        public void ChangeSprite(SpriteLayerID id, LayerState state)
        {
            string newSpritePath = _changeSprite.Execute(id, state);
            _heroinViewModel.UpdateSprite(id, newSpritePath);
        }
    }
}
