// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Threading.Tasks;
using UnityEngine;
using Yarn.Unity;
using Zenject;
using Zrushy.Core.Application.UseCase.ApplyBonus;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Presentation.Unity
{
    /// <summary>
    /// Yarn Spinner カスタムコマンドのハブ
    /// apply_bonus / wait_for_touch / force_touch の 3 コマンドを処理する
    ///
    /// [YarnCommand] 属性をインスタンスメソッドに付けると Yarn Spinner が
    /// 最初の引数を GameObject 名として検索するため、AddCommandHandler で登録する。
    /// </summary>
    public class ScenarioCommandHandler : MonoBehaviour
    {
        [Inject] private ApplyBonus _applyBonusUseCase;
        [Inject] private ScenarioInputGate _scenarioInputGate;
        [Inject] private DialogueRunner _dialogueRunner;
        [Inject] private VirtualCursor _virtualCursor;
        [Inject] private ClickableRegistry _clickableRegistry;

        private void Start()
        {
            _dialogueRunner.AddCommandHandler("apply_bonus",
                (string partId, int amount) => HandleApplyBonus(partId, amount));

            _dialogueRunner.AddCommandHandler("force_touch",
                (string partId) => HandleForceTouch(partId));

            _dialogueRunner.AddCommandHandler("wait_for_touch",
                (string targetPartId) => HandleWaitForTouch(targetPartId));
        }

        /// <summary>
        /// 指定部位の開発度にボーナスを加算する
        /// 使用例: &lt;&lt;apply_bonus secret 10&gt;&gt;
        /// </summary>
        private void HandleApplyBonus(string partId, int amount)
        {
            ApplyBonusCommand command = new ApplyBonusCommand(new PartID(partId), amount);
            _applyBonusUseCase.Execute(command);
        }

        /// <summary>
        /// 次のタッチ入力を待機し、触れられた部位を $touched_part 変数に格納する
        /// 使用例: &lt;&lt;wait_for_touch secret&gt;&gt;
        /// </summary>
        private async Task HandleWaitForTouch(string targetPartId)
        {
            string touched = await _scenarioInputGate.WaitForNextTouch();
            _dialogueRunner.VariableStorage.SetValue("$expected_part", targetPartId);
            _dialogueRunner.VariableStorage.SetValue("$touched_part", touched);
        }

        /// <summary>
        /// 指定部位の Clickable の位置へ仮想カーソルを移動する
        /// 使用例: &lt;&lt;force_touch secret&gt;&gt;
        /// </summary>
        private void HandleForceTouch(string partId)
        {
            if (!_clickableRegistry.TryGet(partId, out Clickable target))
            {
                Debug.LogWarning($"[ScenarioCommandHandler] force_touch: Clickable not found for partId='{partId}'");
                return;
            }
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, target.transform.position);
            _virtualCursor.MoveTo(screenPos);
        }
    }
}
