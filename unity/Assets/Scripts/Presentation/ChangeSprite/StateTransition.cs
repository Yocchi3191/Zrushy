using System;
using UnityEngine;
using Zrushy.Core.Application.UseCase.CanZrushy;
using Zrushy.Core.Presentation.Unity.ChangeSprite;
using Vector2 = System.Numerics.Vector2;

namespace Zrushy.Core.Presentation.Unity
{
	/// <summary>
	/// 1ステートの遷移条件と遷移先を定義するクラス
	/// </summary>
	[Serializable]
	public class StateTransition
	{
		public Sprite fromState; // 遷移元スプライト
		public CardinalDirection requiredDirection;
		public Sprite toState; // 遷移先スプライト

		/// <summary>
		/// ステート遷移の条件を満たすおさわりかを判定する
		/// </summary>
		/// <param name="type">さわりタイプ</param>
		/// <param name="direction">ドラッグ方向</param>
		/// <param name="setting">ドラッグ方向の閾値設定</param>
		/// <returns>ステート遷移可能かどうか</returns>
		internal bool CanTransition(ZrushyInput input, DragDirectionThresholdSetting setting)
		{
			float dot = Vector2.Dot(input.Direction, required);
			return dot >= setting.DragDirectionThreshold;
		}

		private Vector2 required
		{
			get
			{
				return requiredDirection switch
				{
					CardinalDirection.Up => new Vector2(0, 1),
					CardinalDirection.Down => new Vector2(0, -1),
					CardinalDirection.Left => new Vector2(-1, 0),
					CardinalDirection.Right => new Vector2(1, 0),
					_ => throw new ArgumentOutOfRangeException()
				};
			}
		}
	}
}
