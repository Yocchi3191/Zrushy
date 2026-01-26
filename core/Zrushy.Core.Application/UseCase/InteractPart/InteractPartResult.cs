using Zrushy.Core.Domain.Entity;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	/// <summary>
	/// 部位をさわる操作の実行結果
	/// </summary>
	public class InteractPartResult
	{
		/// <summary>
		/// リアクション
		/// </summary>
		public Reaction? Reaction { get; }

		/// <summary>
		/// イベント（発生しない場合はnull）
		/// </summary>
		public Event? Event { get; }

		public InteractPartResult(Reaction? reaction, Event? evt)
		{
			Reaction = reaction;
			Event = evt;
		}
	}
}
