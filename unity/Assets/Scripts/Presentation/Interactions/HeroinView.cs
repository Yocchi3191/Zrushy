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
        [Inject]
        private HeroinViewModel _viewModel;

        [SerializeField] private SpriteLayerBindings[] _bindings;

        private void Start()
        {
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
                SpriteLayerID layerID = path.Key;
                SpriteLayerBindings binding = _bindings.FirstOrDefault(b => b.LayerID == layerID.value);

                if (binding.Image == null)
                    continue;

                if (path.Value.EndsWith("/none"))
                {
                    binding.Image.enabled = false;
                    continue;
                }

                Sprite sprite = Resources.Load<Sprite>(path.Value);
                if (sprite == null)
                {
                    Debug.LogWarning($"Sprite not found at path '{path.Value}' for layer '{layerID.value}'");
                    continue;
                }
                binding.Image.enabled = true;
                binding.Image.sprite = Resources.Load<Sprite>(path.Value);
            }
        }

        [System.Serializable]
        private struct SpriteLayerBindings
        {
            public string LayerID;
            public Image Image;
        }
    }
}
