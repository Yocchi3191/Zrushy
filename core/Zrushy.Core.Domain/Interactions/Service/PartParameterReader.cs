// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Service
{
    public interface IPartParameterReader
    {
        Arousal GetArousal(PartID partID);
        Development GetDevelopment(PartID partID);
        Affection GetAffection(PartID partID);
    }
}
