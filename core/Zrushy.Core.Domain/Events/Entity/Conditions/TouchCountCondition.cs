// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
    /// <summary>
    /// さわり回数が指定した回数以上のときに真となる条件
    /// </summary>
    internal class TouchCountCondition : ICondition
    {
        private readonly IInteractionHistory _history;
        private readonly PartID _partID;
        private readonly int _minCount;

        public TouchCountCondition(IInteractionHistory history, PartID partID, int minCount)
        {
            _history = history;
            _partID = partID;
            _minCount = minCount;
        }

        public bool CanFire()
        {
            return _history.Get(_partID).Count >= _minCount;
        }
    }
}
