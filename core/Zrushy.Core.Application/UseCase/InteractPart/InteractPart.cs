using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	/// <summary>
	/// 部位をさわる操作のユースケース
	/// </summary>
	public class InteractPart
	{
		private readonly Body body;
		private readonly IReactionRepository reactionRepository;
		private readonly IEventRepository eventRepository;

		public InteractPart(
			Body body,
			IReactionRepository reactionRepository,
			IEventRepository eventRepository)
		{
			this.body = body;
			this.reactionRepository = reactionRepository;
			this.eventRepository = eventRepository;
		}

		/// <summary>
		/// 部位をさわる操作を実行する
		/// </summary>
		/// <param name="command">操作コマンド</param>
		/// <returns>実行結果（ReactionとEvent）</returns>
		public InteractPartResult Execute(InteractPartCommand command)
		{
			// 1. 部位にさわる
			Interaction interaction = new Interaction(command.PartID);

			// 2. 部位に対してポイントがたまる
			body.Interact(interaction);

			// さわった部位のパラメータを取得
			Part part = body.GetPart(command.PartID);

			// 3. さわった部位の、現在のパラメータ状態で返すリアクションを取得
			Reaction reaction = reactionRepository.GetReaction(
				command.PartID,
				part.Pleasure,
				part.Development,
				part.Affection
			);

			// 4. 〃で発生するイベントを取得
			Event? evt = eventRepository.GetEvent(
				command.PartID,
				part.Pleasure,
				part.Development,
				part.Affection
			);

			return new InteractPartResult(reaction, evt);
		}
	}
}
