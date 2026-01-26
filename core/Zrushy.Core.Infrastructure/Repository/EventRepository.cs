using System;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
	/// <summary>
	/// イベントリポジトリの実装
	/// 初期実装ではイベント発生条件をチェックしない
	/// </summary>
	public class EventRepository : IEventRepository
	{
		public Event? GetEvent(PartID partID, Pleasure pleasure, Development development, Affection affection)
		{
			// TODO: 実際のイベント発生条件チェックロジックを実装
			// 現在は常にnullを返す（イベント未発生）
			return null;
		}
	}
}
