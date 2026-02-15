using System;
using System.Threading.Tasks;
using UnityEngine;
using Yarn.Unity;
using Zenject;
using Zrushy.Core.Application.UseCase.ApplyBonus;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Presentation;

namespace Zrushy.Unity.Presentation
{
	/// <summary>
	/// Yarn Spinner カスタムコマンドのハブ
	/// apply_bonus / wait_for_touch / force_touch の 3 コマンドを処理する
	/// </summary>
	public class ScenarioCommandHandler : MonoBehaviour
	{
		[Inject] private ApplyBonus applyBonusUseCase;
		[Inject] private ScenarioInputGate scenarioInputGate;
		[Inject] private DialogueRunner dialogueRunner;
		[Inject] private VirtualCursor virtualCursor;

		/// <summary>
		/// 指定部位の開発度にボーナスを加算する
		/// 使用例: &lt;&lt;apply_bonus secret 10&gt;&gt;
		/// </summary>
		[YarnCommand("apply_bonus")]
		public void ApplyBonus(string partId, int amount)
		{
			var command = new ApplyBonusCommand(new PartID(partId), amount);
			applyBonusUseCase.Execute(command);
		}

		/// <summary>
		/// 次のタッチ入力を待機し、触れられた部位を $touched_part 変数に格納する
		/// 使用例: &lt;&lt;wait_for_touch secret&gt;&gt;
		/// </summary>
		[YarnCommand("wait_for_touch")]
		public async Task WaitForTouch(string targetPartId)
		{
			var touched = await scenarioInputGate.WaitForNextTouch();
			dialogueRunner.VariableStorage.SetValue("$touched_part", touched);
		}

		/// <summary>
		/// 指定部位の Clickable の位置へ仮想カーソルを移動する
		/// 使用例: &lt;&lt;force_touch secret&gt;&gt;
		/// </summary>
		[YarnCommand("force_touch")]
		public void ForceTouch(string partId)
		{
			var clickables = FindObjectsByType<Clickable>(FindObjectsSortMode.None);
			var target = Array.Find(clickables, c => c.PartId == partId);
			if (target == null)
			{
				Debug.LogWarning($"[ScenarioCommandHandler] force_touch: Clickable not found for partId='{partId}'");
				return;
			}
			var screenPos = RectTransformUtility.WorldToScreenPoint(null, target.transform.position);
			virtualCursor.MoveTo(screenPos);
		}
	}
}
