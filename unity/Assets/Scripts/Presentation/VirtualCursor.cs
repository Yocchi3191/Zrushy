using UnityEngine;
using UnityEngine.UI;

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
		rectTransform.position = Input.mousePosition;
	}

	/// <summary>
	/// カーソルを指定スクリーン座標へ移動する（force_touch から呼ばれる）
	/// </summary>
	/// <param name="screenPos">移動先のスクリーン座標</param>
	public void MoveTo(Vector2 screenPos)
	{
		rectTransform.position = screenPos;
	}

	private void OnDestroy()
	{
		Cursor.visible = true;
	}
}
