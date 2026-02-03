using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Repository
{
	/// <summary>
	/// イベントリポジトリのインターフェース
	/// イベントデータの取得を抽象化する
	/// </summary>
	public interface IEventRepository
	{
		/// <summary>
		/// 指定した部位とパラメータに基づいてイベントを取得する
		/// </summary>
		/// <param name="partID">部位ID</param>
		/// <param name="pleasure">快感値</param>
		/// <param name="development">開発度</param>
		/// <param name="affection">好感度</param>
		/// <returns>条件に合致したイベント（発生しない場合はnull）</returns>
		IReadOnlyList<IEvent> GetEvents(PartID partID);
	}
}
