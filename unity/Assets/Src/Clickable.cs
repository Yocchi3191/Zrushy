using UnityEngine;
using Zrushy.Core.Presentation;

/// <summary>
/// クリック入力の受け付け
/// 入力に応じた反応の送信
/// </summary>
public class Clickable : MonoBehaviour
{
	PartController controller;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnClick()
	{
		Debug.Log("Clickable: OnClick");
		PartInput input = new PartInput();
		controller.SendInput(input);
	}
}
