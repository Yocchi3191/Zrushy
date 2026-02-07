using System.Collections;
using UnityEngine;
using Zenject;
using Zrushy.Core.Presentation;

/// <summary>
/// シナリオの自動進行を管理
/// 将来的には IEvent からの通知で進行するが、現在は仮実装として一定時間ごとに進行
/// </summary>
public class ScenarioDriver : MonoBehaviour
{
	[Inject]
	private ScenarioPlayer scenarioPlayer;

	[SerializeField]
	[Tooltip("シナリオ自動進行の間隔（秒）。0以下で自動進行無効")]
	private float autoAdvanceInterval = 2.0f;

	private Coroutine autoAdvanceCoroutine;

	private void Start()
	{
		// ScenarioPlayer のイベントを購読
		scenarioPlayer.OnScenarioStarted += StartAutoAdvance;
		scenarioPlayer.OnScenarioFinished += StopAutoAdvance;
	}

	/// <summary>
	/// シナリオの自動進行を開始
	/// </summary>
	public void StartAutoAdvance()
	{
		StopAutoAdvance();
		if (autoAdvanceInterval > 0)
		{
			autoAdvanceCoroutine = StartCoroutine(AutoAdvanceCoroutine());
		}
	}

	/// <summary>
	/// シナリオの自動進行を停止
	/// </summary>
	public void StopAutoAdvance()
	{
		if (autoAdvanceCoroutine != null)
		{
			StopCoroutine(autoAdvanceCoroutine);
			autoAdvanceCoroutine = null;
		}
	}

	private IEnumerator AutoAdvanceCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(autoAdvanceInterval);

			// TODO: 将来的には IEvent からの通知で進行
			// 例: IEventRepository.Subscribe(OnEventFired)
			scenarioPlayer.Next();
		}
	}

	private void OnDestroy()
	{
		// イベント購読を解除
		if (scenarioPlayer != null)
		{
			scenarioPlayer.OnScenarioStarted -= StartAutoAdvance;
			scenarioPlayer.OnScenarioFinished -= StopAutoAdvance;
		}

		StopAutoAdvance();
	}
}
