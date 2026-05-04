// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Infrastructure.Unity
{
    public class SpriteLayerRepository : ISpriteLayerRepository
    {
        private readonly string _resourceRootPath = "Heroin/Sprites";
        public string Get(SpriteLayerID id, LayerState state)
        {
            return $"{_resourceRootPath}/{id.value}/{state.type}";
        }
    }
}
