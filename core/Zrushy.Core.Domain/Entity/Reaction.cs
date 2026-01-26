namespace Zrushy.Core.Domain.Entity
{
	/// <summary>
	/// リアクションエンティティ
	/// 部位をさわった際のキャラクターの反応を表現する
	/// </summary>
	public class Reaction
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

		/// <summary>
		/// ボイスクリップ名
		/// </summary>
		public string VoiceClipName { get; }

		public Reaction(string dialogue, string animationName, string expressionName, string voiceClipName)
		{
			Dialogue = dialogue;
			AnimationName = animationName;
			ExpressionName = expressionName;
			VoiceClipName = voiceClipName;
		}
	}
}
