using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zrushy.Core.Application.UseCase.CanZrushy;

namespace Zrushy.Core.Presentation.Unity.ChangeSprite
{
	/// <summary>
	/// スプライトの状態遷移を実行するランタイム
	/// </summary>
	public class SpriteState : MonoBehaviour, ISpriteInputHandler, ISpriteStateNode
	{
		[SerializeField] private SpriteStatePattern statePattern;
		Image image;
		private Sprite currentState;
		[SerializeField] private DragDirectionThresholdSetting setting;

		public event Action<Sprite> OnStateChanged; // 状態遷移イベント
		public Sprite CurrentState => currentState;

		private void Awake()
		{
			image = gameObject.GetComponent<Image>();
			if (image == null)
				throw new System.Exception("Imageコンポーネントがアタッチされていません");
		}

		void Start()
		{
			currentState = statePattern.initialState;
		}

		/// <summary>
		/// 入力がスプライト遷移条件を満たすか判定し、満たすなら状態遷移させる
		/// </summary>
		public void TryTransition(ZrushyInput input)
		{
			var matched = statePattern.transitions
				.Where(t => t.fromState == currentState)
				.FirstOrDefault(t => t.CanTransition(input, setting));

			if (matched == null) return;

			ForceState(matched.toState);
		}

		/// <summary>
		/// 外部から初期状態に強制リセットする（コーディネーターによるカスケード用）
		/// </summary>
		public void ForceState(Sprite newState)
		{
			currentState = newState;
			image.sprite = currentState;
			OnStateChanged?.Invoke(currentState);
		}

		public bool IsAbove(Sprite maxAllowed)
		{
			throw new NotImplementedException();
		}
	}
}
