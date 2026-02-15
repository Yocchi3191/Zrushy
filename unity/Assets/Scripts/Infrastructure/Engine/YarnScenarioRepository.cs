using System.Linq;
using Yarn.Unity;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

public class YarnScenarioRepository : IScenarioRepository
{
	private readonly DialogueRunner dialogueRunner;
	private readonly ZrushyDialoguePresenter dialoguePresenter;

	private Beat currentBeat;
	private bool isFinished;

	public bool IsScenarioFinished => isFinished;
	public event System.Action<Beat> OnBeatChanged;

	public YarnScenarioRepository(DialogueRunner dialogueRunner, ZrushyDialoguePresenter presenter)
	{
		this.dialogueRunner = dialogueRunner;
		this.dialoguePresenter = presenter;

		presenter.Initialize(this);
	}

	public void Start(ScenarioID scenarioID)
	{
		if (dialogueRunner.YarnProject == null ||
			!dialogueRunner.YarnProject.NodeNames.Contains(scenarioID.Value))
		{
			throw new ScenarioNotFoundException(scenarioID);
		}

		isFinished = false;
		dialogueRunner.StartDialogue(scenarioID.Value);
	}

	public Beat GetCurrentBeat() => currentBeat;

	public void Next()
	{
		dialoguePresenter.Advance();
	}

	/// <summary>
	/// Yarn の Line → Action 変換
	/// </summary>
	internal void SetLineAsAction(LocalizedLine line)
	{
		string dialogue = line.TextWithoutCharacterName.Text;
		string anim = GetMetadata(line, "anim", "reaction_default");
		string expr = GetMetadata(line, "expr", "expression_neutral");
		currentBeat = new Beat(dialogue, anim, expr);
		OnBeatChanged?.Invoke(currentBeat);
	}

	internal void MarkFinished()
	{
		isFinished = true;
	}

	public void Stop()
	{
		dialogueRunner.Stop();
	}

	private string GetMetadata(LocalizedLine line, string key, string fallback)
	{
		foreach (var tag in line.Metadata)
		{
			if (tag.StartsWith(key + ":"))
				return tag.Substring(key.Length + 1);
		}
		return fallback;
	}
}
