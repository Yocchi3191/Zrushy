using System.Collections.Generic;
using System.Linq;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
	/// <summary>
	/// 部位をさわる操作のユースケース
	/// </summary>
	public class InteractPart
	{
		private static readonly PartID BodyPartID = new PartID("body");

		private readonly Heroin body;
		private readonly IEventRepository eventRepository;
		private readonly IEventBus eventBus;
		private readonly IInteractionHistory interactionHistory;

		public InteractPart(
			Heroin body,
			IEventRepository eventRepository,
			IEventBus eventBus,
			IInteractionHistory interactionHistory)
		{
			this.body = body;
			this.eventRepository = eventRepository;
			this.eventBus = eventBus;
			this.interactionHistory = interactionHistory;
		}

		/// <summary>
		/// 部位をさわる操作を実行する
		/// </summary>
		/// <param name="command">操作コマンド</param>
		public void Execute(InteractPartCommand command)
		{
			Interaction interaction = new Interaction(command.PartID, command.Type);
			body.Interact(interaction);
			interactionHistory.Record(interaction);

			var partEvents = eventRepository.GetEvents(command.PartID);
			var bodyEvents = eventRepository.GetEvents(BodyPartID);
			IEnumerable<IScenarioEvent> candidates = partEvents.Concat(bodyEvents);

			IScenarioEvent fired = candidates
				.Where(e => e.CanFire())
				.OrderByDescending(e => e.Priority)
				.FirstOrDefault();

			if (fired != null)
				eventBus.Publish(fired);

			body.ApplyCooldownIfClimax();
		}
	}
}
