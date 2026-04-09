// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.InteractPart
{
    public record InteractPartCommand(PartID PartID, InteractionType Type = InteractionType.Finger);
}
