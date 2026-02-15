using UnityEngine;
using Zenject;
using Zrushy.Core.Presentation;

namespace Zrushy.Presentation.Unity
{
    public class ScenarioPlayingMessage : MonoBehaviour
    {
        [Inject]
        private ScenarioPlayer scenarioPlayer;

        private void OnEnable()
        {
            scenarioPlayer.OnScenarioStarted += Show;
            scenarioPlayer.OnScenarioFinished += Hide;
            gameObject.SetActive(scenarioPlayer.IsPlaying);
        }

        private void OnDisable()
        {
            scenarioPlayer.OnScenarioStarted -= Show;
            scenarioPlayer.OnScenarioFinished -= Hide;
        }

        private void Show() => gameObject.SetActive(true);
        private void Hide() => gameObject.SetActive(false);
    }
}
