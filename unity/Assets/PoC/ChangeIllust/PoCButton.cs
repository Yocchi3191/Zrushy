using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Sprite;
using Zrushy.Core.Presentation;

public class PoCButton : MonoBehaviour
{
	[Inject] private SpriteLayerController _controller;
	[SerializeField] private string _layerID;
	[SerializeField] private string _state;

	public void OnClick()
	{
		SpriteLayerID layerID = new SpriteLayerID(_layerID);
		LayerState state = new LayerState(_state);

		_controller.ChangeSprite(layerID, state);
	}

	private void OnValidate()
	{
		GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{_layerID}_{_state}";
	}
}
