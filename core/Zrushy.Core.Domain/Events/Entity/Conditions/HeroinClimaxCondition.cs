// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.Entity;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
    internal class HeroinClimaxCondition : ICondition
    {
        private readonly Heroin _heroin;

        public HeroinClimaxCondition(Heroin heroin)
        {
            _heroin = heroin;
        }

        public bool CanFire() => _heroin.IsClimax;
    }
}
