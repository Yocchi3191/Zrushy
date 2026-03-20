using System;
using Yarn.Unity;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.ValueObject;

public class YarnBeatProvider : IBeatProvider
{
	private readonly DialogueRunner dialogueRunner;
	private readonly ZrushyDialoguePresenter dialoguePresenter;

	public YarnBeatProvider(DialogueRunner dialogueRunner, ZrushyDialoguePresenter presenter)
	{
		this.dialogueRunner = dialogueRunner;
		this.dialoguePresenter = presenter;

		dialoguePresenter.OnLineReady += line => Current = ConvertToBeat(line);
		dialoguePresenter.OnDialogueCompleted += () => OnCompleted?.Invoke();
	}

	private Beat ConvertToBeat(LocalizedLine line)
	{
		string dialogue = line.TextWithoutCharacterName.Text;
		string anim = GetMetaData(line, "anim", "reaction_default");
		string expr = GetMetaData(line, "expr", "expression_neutral");
		return new Beat(dialogue, anim, expr);
	}

	private string GetMetaData(LocalizedLine line, string key, string fallback)
	{
		foreach (var tag in line.Metadata)
		{
			if (tag.StartsWith(key + ":"))
				return tag.Substring(key.Length + 1);
		}
		return fallback;
	}

	public Beat Current { get; private set; }

	public event Action OnCompleted;

	public void Advance()
	{
		dialoguePresenter.Advance();
	}

	public void Start(ScenarioID id)
	{
		dialogueRunner.StartDialogue(id.Value);
	}
}
