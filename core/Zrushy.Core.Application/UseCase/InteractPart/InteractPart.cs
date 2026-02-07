using System.Collections.Generic;
using System.Linq;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Exception;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	/// <summary>
	/// 部位をさわる操作のユースケース
	/// </summary>
	public class InteractPart : IInteractPart
	{
		private readonly Body body;
		private readonly IEventRepository eventRepository;
		private readonly IEventBus eventBus;

		public InteractPart(
			Body body,
			IEventRepository eventRepository,
			IEventBus eventBus)
		{
			this.body = body;
			this.eventRepository = eventRepository;
			this.eventBus = eventBus;
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

			// EventBus にイベントを発火
			eventBus.Publish(fired);

			return new InteractPartResult(fired.ScenarioToStart);
		}
	}
}
