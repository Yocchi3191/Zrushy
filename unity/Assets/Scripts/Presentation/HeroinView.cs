using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Sprite;
using Zrushy.Core.Presentation;

public class HeroinView : MonoBehaviour
{
	[Inject]
	private HeroinViewModel viewModel;

	private Beat currentBeat;
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
		PlayBeat(vm.CurrentBeat);
		UpdateSpriteLayers(vm.SpritePaths);
	}

	private void PlayBeat(Beat beat)
	{
		this.currentBeat = beat;
		if (this.currentBeat != null)
		{
			Debug.Log($"[Heroin] {this.currentBeat.Dialogue} (anim: {this.currentBeat.AnimationName}, expr: {this.currentBeat.ExpressionName})");
		}
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
