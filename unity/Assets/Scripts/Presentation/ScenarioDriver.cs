using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;

namespace Zrushy.Unity.Presentation
{
	/// <summary>
	/// シナリオの進行を管理
	/// EventBus からのイベントを受け取り、シナリオの開始・進行を行う
	/// </summary>
	public class ScenarioDriver : MonoBehaviour
	{
		[Inject]
		private ScenarioPlayer scenarioPlayer;

		[Inject]
		private IEventBus eventBus;

		/// <summary>
		/// この優先度以上のイベントは現在のシナリオを割り込む
		/// </summary>
		private const int INTERRUPT_PRIORITY_THRESHOLD = 900;

		private void Start()
		{
			eventBus.OnEventPublished += OnEventFired;
		}

		/// <summary>
		/// EventBus からイベントが発火されたときの処理
		/// 高優先度イベントは現在のシナリオを割り込み、通常イベントは進行または開始
		/// </summary>
		private void OnEventFired(IScenarioEvent gameEvent)
		{
			if (gameEvent.Priority >= INTERRUPT_PRIORITY_THRESHOLD && scenarioPlayer.IsPlaying)
			{
				scenarioPlayer.Stop();
				scenarioPlayer.Play(gameEvent.ScenarioToStart);
			}
			else if (!scenarioPlayer.IsPlaying)
			{
				scenarioPlayer.Play(gameEvent.ScenarioToStart);
			}
			// 再生中のタッチは無視（InteractPart でパラメータは加算済み）
		}

		private void OnDestroy()
		{
			if (eventBus != null)
				eventBus.OnEventPublished -= OnEventFired;
		}
	}
}
