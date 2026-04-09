// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Infrastructure.Repository
{
    public class InteractionHistory : IInteractionHistory
    {
        private readonly Dictionary<PartID, List<Interaction>> _history = new Dictionary<PartID, List<Interaction>>();

        public IReadOnlyList<Interaction> Get(PartID partID)
        {
            if (_history.TryGetValue(partID, out List<Interaction>? interactions))
            {
                return interactions;
            }

            return new List<Interaction>();
        }

        public void Record(Interaction interaction)
        {
            if (!_history.ContainsKey(interaction.PartID))
            {
                _history[interaction.PartID] = new List<Interaction>();
            }

            _history[interaction.PartID].Add(interaction);
        }
    }
}
