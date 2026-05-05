// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Application.UseCase.FindSprite
{
    public class FindSprite : IFindSprite
    {

        private readonly ISpriteLayerRepository _repository;
        public FindSprite(ISpriteLayerRepository repository)
        {
            _repository = repository;
        }

        public string Execute(SpriteLayerID id, LayerState state)
        {
            return _repository.Get(id, state);
        }
    }
}
