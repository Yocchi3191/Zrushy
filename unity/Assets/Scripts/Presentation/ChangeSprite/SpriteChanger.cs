// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// スプライト変更機能を提供するコンポーネント
    /// </summary>
    public class SpriteChanger : MonoBehaviour, ISpriteChanger
    {
        [Inject] ISpriteLayerController _controller;
        [SerializeField] string _layerID;
        private SpriteLayerID _spriteLayerID;

        private void Awake()
        {
            _spriteLayerID = new SpriteLayerID(_layerID);
        }

        public void ChangeSprite(string spriteName)
        {
            _controller.ChangeSprite(_spriteLayerID, new LayerState(spriteName));
        }
    }
}
