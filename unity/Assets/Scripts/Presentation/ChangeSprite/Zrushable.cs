using UnityEngine;
using UnityEngine.EventSystems;
using Zrushy.Core.Application.UseCase.CanZrushy;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Vector2 = System.Numerics.Vector2;

namespace Zrushy.Core.Presentation.Unity
{
	public class Zrushable : MonoBehaviour
	{
		private ISpriteInputHandler _spriteInputHandler;
		private IZrushyPermission _zrushyPermission;
		internal void Construct(ISpriteInputHandler spriteInputHandler, IZrushyPermission zrushyPermission)
		{
			_spriteInputHandler = spriteInputHandler;
			_zrushyPermission = zrushyPermission;
		}

		internal void OnPointerClick(PointerEventData pointerEventData)
		{
			InteractionType type = pointerEventData.button == PointerEventData.InputButton.Right
				? InteractionType.Tongue
				: InteractionType.Finger;
			PartInput input = new PartInput(new PartID(gameObject.name), type, Vector2.Zero);

			if (!_zrushyPermission.CanZrushy())
				return;

			_spriteInputHandler.TryTransition(input);
		}

		// Start is called once before the first execution of Update after the MonoBehaviour is created
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}
