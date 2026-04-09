// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service
{
    public interface IInteractionHistory
    {
        IReadOnlyList<Interaction> Get(PartID partID);
        void Record(Interaction interaction);
    }
}
