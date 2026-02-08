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

		private void Start()
		{
			eventBus.OnEventPublished += OnEventFired;
		}

		/// <summary>
		/// EventBus からイベントが発火されたときの処理
		/// 未再生なら開始、再生中なら次へ進もうとする（条件が満たされなければ保留）
		/// </summary>
		private void OnEventFired(IScenarioEvent gameEvent)
		{
			if (scenarioPlayer.IsPlaying)
				scenarioPlayer.Next();
			else
				scenarioPlayer.Play(gameEvent.ScenarioToStart);
		}

		private void OnDestroy()
		{
			if (eventBus != null)
				eventBus.OnEventPublished -= OnEventFired;
		}
	}
}
