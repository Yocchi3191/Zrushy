using System;
using UnityEngine;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Presentation.Unity.ChangeSprite;

namespace Zrushy.Core.Presentation.Unity
{
	/// <summary>
	/// 1ステートの遷移条件と遷移先を定義するクラス
	/// </summary>
	[Serializable]
	public class StateTransition
	{
		public Sprite fromState; // 遷移元スプライト
		public InteractionType requiredType;
		public CardinalDirection requiredDirection;
		public Sprite toState; // 遷移先スプライト

		/// <summary>
		/// ステート遷移の条件を満たすおさわりかを判定する
		/// </summary>
		/// <param name="type">さわりタイプ</param>
		/// <param name="direction">ドラッグ方向</param>
		/// <param name="setting">ドラッグ方向の閾値設定</param>
		/// <returns>ステート遷移可能かどうか</returns>
		internal bool CanTransition(InteractionType type, Vector2 direction, DragDirectionThresholdSetting setting)
		{
			if (type != requiredType) return false;

			if (requiredType == InteractionType.Stroke)
			{
				Vector2 required = requiredDirection switch
				{
					CardinalDirection.Up => Vector2.up,
					CardinalDirection.Down => Vector2.down,
					CardinalDirection.Left => Vector2.left,
					CardinalDirection.Right => Vector2.right,
					_ => Vector2.zero
				};

				float dot = Vector2.Dot(direction.normalized, required);
				return dot >= setting.DragDirectionThreshold;
			}

			return true;
		}
	}
}
