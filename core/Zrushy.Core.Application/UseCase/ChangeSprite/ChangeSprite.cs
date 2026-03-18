// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Application.UseCase.ChangeSprite
{
    public class ChangeSprite
    {

        private readonly ISpriteLayerRepository repository;
        public ChangeSprite(ISpriteLayerRepository repository)
        {
            this.repository = repository;
        }

        public string Execute(SpriteLayerID id, ExpressionType type)
        {
            return repository.Get(id, type);
        }
    }
}
