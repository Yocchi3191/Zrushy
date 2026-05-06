// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zrushy.Core.Domain.Sprite;

namespace Zrushy.Core.Presentation.Unity
{
    public class HeroinView : MonoBehaviour
    {
        [Inject] private HeroinViewModel _viewModel;
        private Dictionary<SpriteLayerID, Image> _layerImages;

        private void Start()
        {
            _layerImages = GetComponentsInChildren<SpriteBindingMarker>()
                            .ToDictionary(c => c.LayerID, c => c.Image);

            _viewModel.OnUpdated += OnViewModelUpdated;
        }

        private void OnDestroy()
        {
            _viewModel.OnUpdated -= OnViewModelUpdated;
        }

        private void OnViewModelUpdated(HeroinViewModel vm)
        {
            UpdateSpriteLayers(vm.SpritePaths);
        }

        private void UpdateSpriteLayers(Dictionary<SpriteLayerID, string> paths)
        {
            foreach (var path in paths)
            {
                if (!_layerImages.TryGetValue(path.Key, out Image image))
                    continue;

                if (path.Value.EndsWith("/none"))
                {
                    image.enabled = false;
                    continue;
                }

                Sprite sprite = Resources.Load<Sprite>(path.Value);
                if (sprite == null)
                {
                    Debug.LogWarning($"Sprite not found at path '{path.Value}' for layer '{path.Key.value}'");
                    continue;
                }
                image.enabled = true;
                image.sprite = sprite;
            }
        }
    }
}
