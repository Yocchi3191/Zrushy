// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Threading.Tasks;

namespace Zrushy.Core.Presentation
{
    /// <summary>
    /// シナリオ再生中のタッチ入力を待機するゲート
    /// wait_for_touch コマンドが await し、PartController.SendInput から通知を受ける
    /// </summary>
    public class ScenarioInputGate
    {
        private TaskCompletionSource<string>? _tcs;

        public bool IsWaiting { get; private set; }

        /// <summary>
        /// 次のタッチ入力を非同期で待機し、触れられた partId を返す
        /// </summary>
        public async Task<string> WaitForNextTouch()
        {
            IsWaiting = true;
            _tcs = new TaskCompletionSource<string>();
            string result = await _tcs.Task;
            IsWaiting = false;
            return result;
        }

        /// <summary>
        /// タッチが発生したことを通知する（PartController から呼ばれる）
        /// </summary>
        /// <param name="partId">触れられた部位ID文字列</param>
        public void NotifyTouch(string partId)
        {
            if (IsWaiting)
            {
                _tcs?.TrySetResult(partId);
            }
        }
    }
}
