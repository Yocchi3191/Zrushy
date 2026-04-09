// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

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
        [Inject] private EventBus _eventBus;
        [Inject] private GetScenario _getScenario;
        [Inject] private DialogueRunner _dialogueRunner;

        private Priority _currentScenarioPriority;

        private void Start()
        {
            _eventBus.OnEventPublished += OnEventPublished;
        }

        private void OnEventPublished(EventID iD)
        {
            ScenarioInfo scenarioInfo = _getScenario.Execute(iD);

            if (!CanStartNewScenario(scenarioInfo))
                return;

            _dialogueRunner.StartDialogue(scenarioInfo.ScenarioID.Value);
            _currentScenarioPriority = scenarioInfo.Priority;
        }

        private bool CanStartNewScenario(ScenarioInfo newScenarioInfo)
        {
            return !_dialogueRunner.IsDialogueRunning || newScenarioInfo.Priority.CompareTo(_currentScenarioPriority) > 0;
        }
    }
}
