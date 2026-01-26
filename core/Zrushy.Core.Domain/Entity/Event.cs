namespace Zrushy.Core.Domain.Entity
{
	/// <summary>
	/// イベントエンティティ
	/// 特定条件で発生する特殊な演出やシーンを表現する
	/// </summary>
	public class Event
	{
		/// <summary>
		/// イベントID
		/// </summary>
		public string EventID { get; }

		/// <summary>
		/// イベント名
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// イベントの種類
		/// </summary>
		public EventType Type { get; }

		/// <summary>
		/// カットシーン名（カットシーンの場合）
		/// </summary>
		public string CutsceneName { get; }

		public Event(string eventID, string name, EventType type, string cutsceneName = "")
		{
			EventID = eventID;
			Name = name;
			Type = type;
			CutsceneName = cutsceneName;
		}
	}

	/// <summary>
	/// イベントの種類
	/// </summary>
	public enum EventType
	{
		/// <summary>
		/// 初回さわり
		/// </summary>
		FirstTouch,

		/// <summary>
		/// 開発度閾値到達
		/// </summary>
		DevelopmentThreshold,

		/// <summary>
		/// 絶頂
		/// </summary>
		Climax,

		/// <summary>
		/// 条件付け完成
		/// </summary>
		ConditioningComplete,

		/// <summary>
		/// その他
		/// </summary>
		Other
	}
}
