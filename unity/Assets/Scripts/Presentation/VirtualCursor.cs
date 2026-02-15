using UnityEngine;
using UnityEngine.UI;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using System.Runtime.InteropServices;
#endif

/// <summary>
/// OS カーソルを非表示にし、手スプライトをマウス座標に追従させるカスタムカーソル
/// force_touch コマンドから MoveTo で任意の位置へ移動できる
/// </summary>
public class VirtualCursor : MonoBehaviour
{
	private RectTransform rectTransform;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
	[DllImport("user32.dll")]
	private static extern bool SetCursorPos(int X, int Y);

	[DllImport("user32.dll")]
	private static extern bool ClientToScreen(System.IntPtr hwnd, ref WinPoint lpPoint);

	[DllImport("user32.dll")]
	private static extern System.IntPtr GetActiveWindow();

	[StructLayout(LayoutKind.Sequential)]
	private struct WinPoint { public int x, y; }
#endif

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
		if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
			rectTransform.position = Input.mousePosition;
	}

	/// <summary>
	/// 仮想カーソルと OS カーソルを指定スクリーン座標へ移動する（force_touch から呼ばれる）
	/// OS カーソルも移動するため、プレイヤーがそのままクリックすれば対象部位にヒットする
	/// </summary>
	/// <param name="screenPos">移動先の Unity スクリーン座標（左下原点）</param>
	public void MoveTo(Vector2 screenPos)
	{
		rectTransform.position = screenPos;
		MoveOsCursor(screenPos);
	}

	/// <summary>
	/// OS カーソルを Unity スクリーン座標に対応する位置へ移動する
	/// ClientToScreen で Unity クライアント座標→Windows スクリーン座標に変換する
	/// </summary>
	private void MoveOsCursor(Vector2 unityScreenPos)
	{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
		// Unity は左下原点、Windows クライアント座標は左上原点
		var pt = new WinPoint
		{
			x = (int)unityScreenPos.x,
			y = Screen.height - (int)unityScreenPos.y
		};
		ClientToScreen(GetActiveWindow(), ref pt);
		SetCursorPos(pt.x, pt.y);
#endif
	}

	private void OnDestroy()
	{
		Cursor.visible = true;
	}
}
