// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// スプライト変更機能を提供するコンポーネント
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class SpriteChanger : MonoBehaviour, ISpriteChanger
    {
        [Inject] ISpriteLayerController _controller;
        [SerializeField] string _layerID;

        public Image Image { get; private set; }

        public SpriteLayerID LayerID { get; private set; }


        private void Awake()
        {
            LayerID = new SpriteLayerID(_layerID);
            Image = GetComponent<Image>();
        }

        public void ChangeSprite(string spriteName)
        {
            _controller.ChangeSprite(LayerID, new LayerState(spriteName));
        }
    }
}
