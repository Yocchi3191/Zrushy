using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Zrushy.Core.Presentation.Unity
{
	/// <summary>
	/// OS カーソルを非表示にし、手スプライトをマウス座標に追従させるカスタムカーソル
	/// force_touch コマンドから MoveTo で任意の位置へ移動できる
	/// </summary>
	public class VirtualCursor : MonoBehaviour
	{
		private RectTransform rectTransform;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			Cursor.visible = false;

			// 他 UI へのレイキャストをブロックしないよう無効化
			var image = GetComponent<Image>();
			if (image != null)
				image.raycastTarget = false;
		}

		private void Update()
		{
			// マウスが動いたときだけ追従する
			// MoveTo() 呼び出し後にマウスが静止していても上書きしないため
			if (Mouse.current.delta.ReadValue() != Vector2.zero)
				rectTransform.position = Mouse.current.position.ReadValue();
		}

		/// <summary>
		/// 仮想カーソルと OS カーソルを指定スクリーン座標へ移動する（force_touch から呼ばれる）
		/// OS カーソルも移動するため、プレイヤーがそのままクリックすれば対象部位にヒットする
		/// </summary>
		/// <param name="screenPos">移動先の Unity スクリーン座標（左下原点）</param>
		public void MoveTo(Vector2 screenPos)
		{
			rectTransform.position = screenPos;
			Mouse.current.WarpCursorPosition(screenPos);
		}

		private void OnDestroy()
		{
			Cursor.visible = true;
		}
	}
}
