using System.Collections.Generic;
using UnityEngine;

namespace Zrushy.Core.Presentation.Unity
{
	/// <summary>
	/// スプライトの状態遷移パターンを定義する ScriptableObject
	/// </summary>
	[CreateAssetMenu(fileName = "SpriteStatePattern", menuName = "Scriptable Objects/SpriteStatePattern")]
	public class SpriteStatePattern : ScriptableObject
	{
		public string layerID; // 対象レイヤーのID
		public Sprite initialState; // 初期状態のスプライト
		public List<StateTransition> transitions = new(); // 状態遷移のリスト
	}
}
