using UnityEngine;
using Yarn.Unity;
using Zenject;
using Zrushy.Core.Application;
using Zrushy.Core.Application.UseCase.GetScenario;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Infrastructure.Unity
{
	/// <summary>
	/// おさわりのイベント発火に応じてシナリオを開始するクラス
	/// </summary>
	public class YarnScenarioStarter : MonoBehaviour
	{
		[Inject] private EventBus eventBus;
		[Inject] private GetScenario getScenario;
		[Inject] private DialogueRunner dialogueRunner;

		private Priority currentScenarioPriority;

		private void Start()
		{
			eventBus.OnEventPublished += OnEventPublished;
		}

		private void OnEventPublished(EventID iD)
		{
			var scenarioInfo = getScenario.Execute(iD);

			if (!CanStartNewScenario(scenarioInfo))
				return;

			dialogueRunner.StartDialogue(scenarioInfo.ScenarioID.Value);
			currentScenarioPriority = scenarioInfo.Priority;
		}

		private bool CanStartNewScenario(ScenarioInfo newScenarioInfo)
		{
			return !dialogueRunner.IsDialogueRunning || newScenarioInfo.Priority.CompareTo(currentScenarioPriority) > 0;
		}
	}
}
