using UnityEngine;
using Zenject;
using Zrushy.Core.Presentation;

namespace Zrushy.Presentation.Unity
{
	public class ScenarioPlayingMessage : MonoBehaviour
	{
		[Inject]
		private IScenarioAdvancable scenarioPlayer;

		[SerializeField]
		private GameObject target;

		private void OnEnable()
		{
			scenarioPlayer.OnScenarioStarted += Show;
			scenarioPlayer.OnScenarioFinished += Hide;
			target.SetActive(scenarioPlayer.IsPlaying);
		}

		private void OnDisable()
		{
			scenarioPlayer.OnScenarioStarted -= Show;
			scenarioPlayer.OnScenarioFinished -= Hide;
		}

		private void Show() => target.SetActive(true);
		private void Hide() => target.SetActive(false);
	}
}
