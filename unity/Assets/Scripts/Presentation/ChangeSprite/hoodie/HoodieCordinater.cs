using System;
using UnityEngine;
using Zrushy.Core.Presentation.Unity.ChangeSprite;

namespace Zrushy.Core.Presentation.Unity
{
	public class HoodieCordinater : MonoBehaviour
	{
		[SerializeField] Clickable[] zipperClickables;
		[SerializeField] Clickable[] hoodieLClickables;
		[SerializeField] Clickable[] hoodieRClickables;
		[SerializeField] SpriteState hoodieL;
		[SerializeField] SpriteState hoodieR;
		[SerializeField] SpriteState zipper;
		[SerializeField] Sprite zipperClosedSprite;

		private Action<PartInput, Vector2> onZipperInput;
		private Action<PartInput, Vector2> onHoodieLInput;
		private Action<PartInput, Vector2> onHoodieRInput;

		void Start()
		{
			onZipperInput = (input, dir) => zipper.TryTransition(input.Type, dir);
			foreach (var c in zipperClickables)
				c.OnInputSent += onZipperInput;

			onHoodieLInput = (input, dir) =>
			{
				if (zipper.CurrentState != zipperClosedSprite)
					hoodieL.TryTransition(input.Type, dir);
			};
			foreach (var c in hoodieLClickables)
				c.OnInputSent += onHoodieLInput;

			onHoodieRInput = (input, dir) =>
			{
				if (zipper.CurrentState != zipperClosedSprite)
					hoodieR.TryTransition(input.Type, dir);
			};
			foreach (var c in hoodieRClickables)
				c.OnInputSent += onHoodieRInput;

			zipper.OnStateChanged += OnZipperStateChanged;
		}

		void OnDestroy()
		{
			foreach (var c in zipperClickables) c.OnInputSent -= onZipperInput;
			foreach (var c in hoodieLClickables) c.OnInputSent -= onHoodieLInput;
			foreach (var c in hoodieRClickables) c.OnInputSent -= onHoodieRInput;
			zipper.OnStateChanged -= OnZipperStateChanged;
		}

		private void OnZipperStateChanged(Sprite newState)
		{
			if (newState == zipperClosedSprite)
			{
				hoodieL.ResetToInitialState();
				hoodieR.ResetToInitialState();
			}
		}
	}
}
