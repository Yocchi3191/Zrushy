using System;
using UnityEngine;
using Zrushy.Core.Presentation.Unity.ChangeSprite;

namespace Zrushy.Core.Presentation.Unity
{
	/// <summary>
	/// 遷移に条件がある複数のSpriteStateをまとめて管理するクラス
	/// 遷移条件の基準になるControllerStateの変更を受けて、dependents(SpriteState)の状態を強制的に遷移させる
	/// </summary>
	public class HoodieStateMediator : MonoBehaviour, ISpriteStateMediator
	{
		[SerializeField] ConstraintEntry[] constraints; // controllerの状態ごとの、dependentsの遷移可能な状態
		[SerializeField] SpriteState controller;
		[SerializeField] SpriteState[] dependents; // controllerの状態に応じて遷移させるSpriteState

		public void OnStateChanged(SpriteState newState)
		{
			/// controllerが遷移した場合
			/// constrainttsからcontrollerの状態に応じた遷移可能な状態を取得
			/// foreach dependent in dependents
			/// 
			/// もし dependentがなら何もしない
		}

		private bool IsValidState(SpriteState dependent)
		{
			throw new NotImplementedException();
		}
	}
}
