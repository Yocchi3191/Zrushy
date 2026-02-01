using System.Collections.Generic;
using System.Linq;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Exception;
using Zrushy.Core.Domain.Repository;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	/// <summary>
	/// 部位をさわる操作のユースケース
	/// </summary>
	public class InteractPart
	{
		private readonly Body body;
		private readonly IEventRepository eventRepository;

		public InteractPart(
			Body body,
			IEventRepository eventRepository)
		{
			this.body = body;
			this.eventRepository = eventRepository;
		}

		/// <summary>
		/// 部位をさわる操作を実行する
		/// </summary>
		/// <param name="command">操作コマンド</param>
		/// <returns>実行結果（ReactionとEvent）</returns>
		public InteractPartResult Execute(InteractPartCommand command)
		{
			Interaction interaction = new Interaction(command.PartID);
			body.Interact(interaction);

			IEnumerable<IEvent> candidates = eventRepository.GetEvents(command.PartID);

			IEvent fired = candidates
				.Where(e => e.CanFire())
				.OrderByDescending(e => e.Priority)
				.FirstOrDefault()
				?? throw new UndefinedReactionException(command.PartID);

			return new InteractPartResult(fired.ScenarioToStart);
		}
	}
}
