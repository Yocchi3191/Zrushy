using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
	public class InteractionHistory : IInteractionHistory
	{
		private readonly Dictionary<PartID, List<Interaction>> history = new Dictionary<PartID, List<Interaction>>();

		public IReadOnlyList<Interaction> Get(PartID partID)
		{
			if (history.TryGetValue(partID, out var interactions))
				return interactions;
			return new List<Interaction>();
		}

		public void Record(Interaction interaction)
		{
			if (!history.ContainsKey(interaction.PartID))
				history[interaction.PartID] = new List<Interaction>();
			history[interaction.PartID].Add(interaction);
		}
	}
}
