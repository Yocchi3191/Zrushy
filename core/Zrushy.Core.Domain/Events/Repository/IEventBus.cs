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
		event Action<IScenarioEvent>? OnEventPublished;
		void Publish(IScenarioEvent gameEvent);
	}
}
