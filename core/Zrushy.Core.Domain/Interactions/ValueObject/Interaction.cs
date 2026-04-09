// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
    public record Interaction(PartID PartID, InteractionType Type = InteractionType.Finger);
}
