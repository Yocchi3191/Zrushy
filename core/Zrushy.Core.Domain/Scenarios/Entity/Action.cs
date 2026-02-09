namespace Zrushy.Core.Domain.Scenarios.Entity
{
	/// <summary>
	/// リアクションエンティティ
	/// 部位をさわった際のキャラクターの反応を表現する
	/// </summary>
	public record Action(string Dialogue, string AnimationName, string ExpressionName, string? Condition = null);
}
