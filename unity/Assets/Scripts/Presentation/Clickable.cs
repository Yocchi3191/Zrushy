using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Presentation;

/// <summary>
/// クリック入力の受け付け
/// 入力に応じた反応の送信
/// UIのEventSystemを使用してクリックを検知
/// </summary>
public class Clickable : MonoBehaviour, IPointerClickHandler
{
	public string PartId => gameObject.name;

	[Inject]
	private PartController controller;

	/// <summary>
	/// IPointerClickHandlerの実装
	/// EventSystemから自動的に呼ばれる
	/// </summary>
	public void OnPointerClick(PointerEventData eventData)
	{
		OnClick();
	}

	/// <summary>
	/// クリック処理（UIボタンからも呼び出し可能）
	/// </summary>
	public void OnClick()
	{
		PartID partID = new PartID(gameObject.name);
		PartInput input = new PartInput(partID);

		// コントローラーにコマンドを送信（ViewModelを渡す）
		controller.SendInput(input);
	}
}
