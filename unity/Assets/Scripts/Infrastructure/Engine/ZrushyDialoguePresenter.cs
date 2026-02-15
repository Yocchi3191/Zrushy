using System.Threading.Tasks;
using Yarn.Unity;

public class ZrushyDialoguePresenter : DialoguePresenterBase
{
	private YarnScenarioRepository engine;

	// YarnScenarioEngine.Next() が呼ばれるまで待機するための仕組み
	private TaskCompletionSource<bool> waitForNext;

	/// <summary>
	/// YarnScenarioEngine から呼ばれる初期化メソッド
	/// </summary>
	public void Initialize(YarnScenarioRepository engine)
	{
		this.engine = engine;
	}

	public override YarnTask OnDialogueStartedAsync()
	{
		// ダイアログ開始時の処理（必要に応じて実装）
		return YarnTask.CompletedTask;
	}

	public override async YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
	{
		// Line → Action 変換（engine の内部状態を更新）
		engine.SetLineAsAction(line);

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
		engine.MarkFinished();
		return YarnTask.CompletedTask;
	}
}
