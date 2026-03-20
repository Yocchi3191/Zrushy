using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Zrushy.Core.Presentation;

namespace Zrushy.Unity.Presentation
{
	/// <summary>
	/// 画面全体を覆う透明パネルに付与する MonoBehaviour。
	/// クリックによる手動進行と、一定時間後の自動進行を管理する。
	/// </summary>
	public class DialogueAdvancePanel : MonoBehaviour, IPointerClickHandler
	{
		[Inject]
		private IScenarioAdvancable scenarioPlayer;

		private const float AUTO_ADVANCE_INTERVAL = 3f;
		private Coroutine autoAdvanceCoroutine;

		private void OnEnable()
		{
			scenarioPlayer.OnScenarioStarted += StartAutoAdvance;
			scenarioPlayer.OnScenarioFinished += StopAutoAdvance;
		}

		private void OnDisable()
		{
			scenarioPlayer.OnScenarioStarted -= StartAutoAdvance;
			scenarioPlayer.OnScenarioFinished -= StopAutoAdvance;
			StopAutoAdvance();
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			AdvanceDialogue();
		}

		private void AdvanceDialogue()
		{
			if (!scenarioPlayer.IsPlaying) return;
			StopAutoAdvance();
			scenarioPlayer.Next();
			if (scenarioPlayer.IsPlaying) StartAutoAdvance();
		}

		private void StartAutoAdvance()
		{
			StopAutoAdvance();
			autoAdvanceCoroutine = StartCoroutine(AutoAdvanceRoutine());
		}

		private void StopAutoAdvance()
		{
			if (autoAdvanceCoroutine != null)
			{
				StopCoroutine(autoAdvanceCoroutine);
				autoAdvanceCoroutine = null;
			}
		}

		private IEnumerator AutoAdvanceRoutine()
		{
			yield return new WaitForSeconds(AUTO_ADVANCE_INTERVAL);
			if (scenarioPlayer.IsPlaying) AdvanceDialogue();
		}
	}
}
