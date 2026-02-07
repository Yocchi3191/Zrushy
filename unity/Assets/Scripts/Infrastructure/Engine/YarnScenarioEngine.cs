using Yarn.Unity;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;
using Zrushy.Core.Infrastructure;
using Action = Zrushy.Core.Domain.Scenarios.Entity.Action;

public class YarnScenarioEngine : IScenarioEngine
{
	private readonly DialogueRunner dialogueRunner;
	private readonly ZrushyDialoguePresenter dialoguePresenter;

	// Yarn Spinner からの Line を Action に変換してバッファする
	private Action currentAction;
	private bool isFinished;

	public bool IsScenarioFinished => isFinished;

	public YarnScenarioEngine(DialogueRunner dialogueRunner, ZrushyDialoguePresenter presenter)
	{
		this.dialogueRunner = dialogueRunner;
		this.dialoguePresenter = presenter;

		// DialoguePresenter にこのエンジンを設定
		presenter.Initialize(this);
	}

	public void Start(ScenarioID scenarioID)
	{
		isFinished = false;
		// ScenarioID.Value = Yarn ノード名
		dialogueRunner.StartDialogue(scenarioID.Value);
	}

	public Action GetCurrentAction() => currentAction;

	public void Next()
	{
		// 内部の DialoguePresenter に次の行への進行を指示
		dialoguePresenter.Advance();
	}

	/// <summary>
	/// Yarn の Line + ハッシュタグ → Action に変換
	/// DialoguePresenter から呼ばれる（内部メソッド）
	/// </summary>
	internal void SetLineAsAction(LocalizedLine line)
	{
		string dialogue = line.TextWithoutCharacterName.Text;
		string anim = GetMetadata(line, "anim", "reaction_default");
		string expr = GetMetadata(line, "expr", "expression_neutral");
		currentAction = new Action(dialogue, anim, expr);
	}

	/// <summary>
	/// DialoguePresenter から呼ばれる（内部メソッド）
	/// </summary>
	internal void MarkFinished()
	{
		isFinished = true;
	}

	private string GetMetadata(LocalizedLine line, string key, string fallback)
	{
		// line.Metadata からハッシュタグを検索
		foreach (var tag in line.Metadata)
		{
			if (tag.StartsWith(key + ":"))
				return tag.Substring(key.Length + 1);
		}
		return fallback;
	}
}
