using UnityEngine;
using Zrushy.Core.Presentation.Unity.ChangeSprite;

namespace Zrushy.Core.Presentation.Unity
{
	/// <summary>
	/// 遷移に条件がある複数のSpriteStateをまとめて管理するクラス
	/// 遷移条件の基準になるControllerStateの変更を受けて、dependents(SpriteState)の状態を強制的に遷移させる
	/// </summary>
	public class SpriteStateMediator : MonoBehaviour, ISpriteInputHandler
	{
		[SerializeField] ConstraintEntry[] constraints; // controllerの状態ごとの、dependentsの遷移可能な状態
		ISpriteStateRouter router; // Clickable => SpriteState の対応づけがつらいので、routerに任せる

		private void Awake()
		{
			router ??= GetComponent<ISpriteStateRouter>();
		}

		void Start()
		{
			router.Controller.OnStateChanged += OnControllerStateChanged;
		}

		void OnDestroy()
		{
			router.Controller.OnStateChanged -= OnControllerStateChanged;
		}

		internal void Construct(ISpriteStateRouter router, ConstraintEntry[] constraints)
		{
			this.router = router;
			this.constraints = constraints;
		}

		private void OnControllerStateChanged(Sprite newState)
		{
			var entry = System.Array.Find(constraints, c => c.ControllerState == newState);
			if (entry == null) return;

			foreach (var dependent in router.Dependents)
			{
				if (dependent.IsAbove(entry.maxAllowedState))
					dependent.ForceState(entry.maxAllowedState);
			}
		}

		public void TryTransition(PartInput input)
		{
			router.Handle(input);
		}

		/// <summary>
		/// constraints をもとに controller と dependents の状態を比較して
		/// dependents の状態が 現在のcontrollerの状態で取ることのできる状態の場合、true を返す
		/// </summary>
		/// <returns></returns>
		private bool IsValidState(SpriteState dependent)
		{
			return false;
		}
	}
}
