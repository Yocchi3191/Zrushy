using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Presentation;

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
			// 高優先度イベント（絶頂など）は現在のシナリオを割り込む
			if (gameEvent.Priority >= INTERRUPT_PRIORITY_THRESHOLD && scenarioPlayer.IsPlaying)
			{
				scenarioPlayer.Stop(); // 現在のシナリオを強制停止
				scenarioPlayer.Play(gameEvent.ScenarioToStart); // 新しいシナリオを開始
			}
			// 通常の優先度
			else if (scenarioPlayer.IsPlaying)
			{
				scenarioPlayer.Next(); // 次へ進む
			}
			else
			{
				scenarioPlayer.Play(gameEvent.ScenarioToStart); // 新規開始
			}
		}

		private void OnDestroy()
		{
			if (eventBus != null)
				eventBus.OnEventPublished -= OnEventFired;
		}
	}
}
