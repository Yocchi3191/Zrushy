using System.Linq;
using Yarn.Unity;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;
using Zrushy.Core.Infrastructure;
using Zrushy.Core.Infrastructure.Engine;
using Action = Zrushy.Core.Domain.Scenarios.Entity.Action;

public class YarnScenarioEngine : IScenarioEngine
{
	private readonly DialogueRunner dialogueRunner;
	private readonly ZrushyDialoguePresenter dialoguePresenter;
	private readonly IConditionFactory conditionFactory;

	private Action currentAction;
	private bool isFinished;

	public bool IsScenarioFinished => isFinished;
	public IEvent? CurrentProceedCondition { get; private set; }

	public YarnScenarioEngine(DialogueRunner dialogueRunner, ZrushyDialoguePresenter presenter, IConditionFactory conditionFactory)
	{
		this.dialogueRunner = dialogueRunner;
		this.dialoguePresenter = presenter;
		this.conditionFactory = conditionFactory;

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
		CurrentProceedCondition = null;
		dialogueRunner.StartDialogue(scenarioID.Value);
	}

	public Action GetCurrentAction() => currentAction;

	public void Next()
	{
		dialoguePresenter.Advance();
	}

	/// <summary>
	/// Yarn の Line → Action 変換 + 進行条件の解析
	/// </summary>
	internal void SetLineAsAction(LocalizedLine line)
	{
		string dialogue = line.TextWithoutCharacterName.Text;
		string anim = GetMetadata(line, "anim", "reaction_default");
		string expr = GetMetadata(line, "expr", "expression_neutral");
		currentAction = new Action(dialogue, anim, expr);

		string conditionString = GetMetadata(line, "condition", null);
		CurrentProceedCondition = conditionString != null ? conditionFactory.Create(conditionString) : null;
	}

	internal void MarkFinished()
	{
		isFinished = true;
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
