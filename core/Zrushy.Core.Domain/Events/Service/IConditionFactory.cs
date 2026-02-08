using Zrushy.Core.Domain.Events.Entity;

namespace Zrushy.Core.Domain.Events.Service
{
	/// <summary>
	/// 条件文字列からIEventを生成するファクトリ
	/// 単条件の場合はICondition、複数条件の場合はEventを返す
	/// </summary>
	public interface IConditionFactory
	{
		/// <summary>
		/// 条件文字列を解析してIEventを生成する
		/// </summary>
		/// <param name="conditionString">条件文字列 (例: "touch_count:head:5,affection:head:20")</param>
		/// <returns>対応するIEvent。解析できない場合はnull</returns>
		IEvent? Create(string conditionString);
	}
}
