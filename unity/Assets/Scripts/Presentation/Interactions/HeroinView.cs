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
		private HeroinViewModel viewModel;

		[SerializeField] private SpriteLayerBindings[] bindings;

		private void Start()
		{
			viewModel.OnUpdated += OnViewModelUpdated;
		}

		private void OnDestroy()
		{
			viewModel.OnUpdated -= OnViewModelUpdated;
		}

		private void OnViewModelUpdated(HeroinViewModel vm)
		{
			UpdateSpriteLayers(vm.SpritePaths);
		}

		private void UpdateSpriteLayers(Dictionary<SpriteLayerID, string> paths)
		{
			foreach (var path in paths)
			{
				var layerID = path.Key;
				var binding = bindings.FirstOrDefault(b => b.LayerID == layerID.value);

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