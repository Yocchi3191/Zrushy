// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.UI;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// レイヤーIDとImageコンポーネントを紐づけるクラス
    /// Imageの切り替えを行うHeroinViewがマーカーとして利用する
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class SpriteBindingMarker : MonoBehaviour
    {
        [SerializeField] private string _layerID;

        public Image Image { get; private set; }
        public SpriteLayerID LayerID { get; private set; }

        private void Awake()
        {
            Image = GetComponent<Image>();
            LayerID = new SpriteLayerID(_layerID);
        }
    }
}
