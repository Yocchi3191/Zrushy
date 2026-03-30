using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Presentation.Unity.ChangeSprite
{
	/// <summary>
	/// スプライトの状態遷移を実行するランタイム
	/// </summary>
	public class SpriteState : MonoBehaviour
	{
		[SerializeField] private SpriteStatePattern statePattern;
		Image image;
		private Sprite currentState;
		[SerializeField] private DragDirectionThresholdSetting setting;
		[SerializeField] private Clickable[] clickables;

		public event Action<Sprite> OnStateChanged;
		public Sprite CurrentState => currentState;

		private Action<PartInput, Vector2> onInputSent;

		private void Awake()
		{
			image = gameObject.GetComponent<Image>();
			if (image == null)
				throw new System.Exception("Imageコンポーネントがアタッチされていません");
		}

		void Start()
		{
			currentState = statePattern.initialState;

			if (clickables == null || clickables.Length == 0) return;

			onInputSent = (input, direction) => TryTransition(input.Type, direction);
			foreach (var clickable in clickables)
				clickable.OnInputSent += onInputSent;
		}

		void OnDestroy()
		{
			if (clickables == null) return;
			foreach (var clickable in clickables)
				clickable.OnInputSent -= onInputSent;
		}

		/// <summary>
		/// 入力がスプライト遷移条件を満たすか判定し、満たすなら状態遷移させる
		/// </summary>
		public void TryTransition(InteractionType type, Vector2 direction)
		{
			var matched = statePattern.transitions
				.Where(t => t.fromState == currentState)
				.FirstOrDefault(t => t.CanTransition(type, direction, setting));

			if (matched == null) return;

			currentState = matched.toState;
			image.sprite = currentState;
			OnStateChanged?.Invoke(currentState);
		}

		/// <summary>
		/// 外部から初期状態に強制リセットする（コーディネーターによるカスケード用）
		/// </summary>
		public void ResetToInitialState()
		{
			currentState = statePattern.initialState;
			image.sprite = currentState;
			OnStateChanged?.Invoke(currentState);
		}
	}
}
