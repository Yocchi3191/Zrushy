using System;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;

namespace Zrushy.Core.Infrastructure.EventBus
{
	/// <summary>
	/// イベントバス（仮実装）
	/// IEventBus を実装し、IEvent の発火と購読を管理する
	/// TODO: 将来的には優先度、フィルタリング、非同期処理などを追加
	/// </summary>
	public class EventBus : IEventBus
	{
		/// <summary>
		/// イベントが発火されたときに通知されるイベント
		/// </summary>
		public event Action<IEvent>? OnEventPublished;

		/// <summary>
		/// イベントを発火する
		/// </summary>
		public void Publish(IEvent gameEvent)
		{
			if (gameEvent == null || !gameEvent.CanFire())
			{
				return;
			}

			OnEventPublished?.Invoke(gameEvent);
		}
	}
}
