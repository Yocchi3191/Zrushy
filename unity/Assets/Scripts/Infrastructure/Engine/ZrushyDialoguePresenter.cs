using System;
using System.Threading.Tasks;
using Yarn.Unity;

public class ZrushyDialoguePresenter : DialoguePresenterBase
{
	// YarnScenarioEngine.Next() が呼ばれるまで待機するための仕組み
	private TaskCompletionSource<bool> waitForNext;
	public event Action<LocalizedLine> OnLineReady;
	public event Action OnDialogueCompleted;

	public override YarnTask OnDialogueStartedAsync()
	{
		// ダイアログ開始時の処理（必要に応じて実装）
		return YarnTask.CompletedTask;
	}

	public override async YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
	{
		OnLineReady?.Invoke(line);

		// YarnScenarioEngine.Next() が呼ばれるまで待機
		waitForNext = new TaskCompletionSource<bool>();
		await waitForNext.Task;
	}

	/// <summary>
	/// YarnScenarioEngine.Next() から呼ばれる。待機を解除して次の Line に進む
	/// </summary>
	public void Advance()
	{
		waitForNext?.TrySetResult(true);
	}

	public override YarnTask OnDialogueCompleteAsync()
	{
		OnDialogueCompleted?.Invoke();
		return YarnTask.CompletedTask;
	}
}
