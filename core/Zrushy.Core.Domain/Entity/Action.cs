namespace Zrushy.Core.Domain.Entity
{
	/// <summary>
	/// リアクションエンティティ
	/// 部位をさわった際のキャラクターの反応を表現する
	/// </summary>
	public class Action
	{
		/// <summary>
		/// セリフ（テキスト）
		/// </summary>
		public string Dialogue { get; }

		/// <summary>
		/// アニメーション名
		/// </summary>
		public string AnimationName { get; }

		/// <summary>
		/// 表情名
		/// </summary>
		public string ExpressionName { get; }
		public Action(string dialogue, string animationName, string expressionName)
		{
			Dialogue = dialogue;
			AnimationName = animationName;
			ExpressionName = expressionName;
		}
	}
}
