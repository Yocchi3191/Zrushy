// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Interactions.Entity
{
    public record SecretPartConfig(
        int WetThreshold,
        int DryDiscomfort,
        int VirginityLossPenalty,
        int FingerWetBase,
        float FingerWetDevFactor,
        int PenisWetBase,
        float PenisWetDevFactor);
}
