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
		IReadOnlyList<IScenarioEvent> GetEvents(PartID partID);

		/// <summary>
		/// どの部位を触れた場合でも評価対象となるグローバルイベントを返す
		/// </summary>
		IReadOnlyList<IScenarioEvent> GetGlobalEvents();
	}
}
