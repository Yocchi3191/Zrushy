// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Application.UseCase.ChangeSprite;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation
{
    public class SpriteLayerController
    {
        private readonly ChangeSprite changeSprite;
        private readonly HeroinViewModel heroinViewModel;
        public SpriteLayerController(ChangeSprite changeSprite, HeroinViewModel heroinViewModel)
        {
            this.changeSprite = changeSprite;
            this.heroinViewModel = heroinViewModel;
        }
        public void ChangeSprite(SpriteLayerID id, ExpressionType type)
        {
            string newSpritePath = changeSprite.Execute(id, type);
            heroinViewModel.UpdateSprite(id, newSpritePath);
        }
    }
}
