using System;
using Zrushy.Core.Domain.Events.Entity;

namespace Zrushy.Core.Domain.Events.Repository
{
	/// <summary>
	/// イベントバスのインターフェース
	/// イベントの発火と購読を抽象化する
	/// </summary>
	public interface IEventBus
	{
		/// <summary>
		/// イベントが発火されたときに通知されるイベント
		/// </summary>
		event Action<IEvent>? OnEventPublished;

		/// <summary>
		/// イベントを発火する
		/// </summary>
		void Publish(IEvent gameEvent);
	}
}
