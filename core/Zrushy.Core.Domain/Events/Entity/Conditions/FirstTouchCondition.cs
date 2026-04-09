// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Entity.Conditions
{
    /// <summary>
    /// 対応する部位への初回おさわり時に真となる条件
    /// </summary>
    internal class FirstTouchCondition : ICondition
    {
        private readonly IInteractionHistory _history;
        private readonly PartID _partID;

        public FirstTouchCondition(IInteractionHistory history, PartID partID)
        {
            _history = history;
            _partID = partID;
        }

        public bool CanFire()
        {
            return _history.Get(_partID).Count == 1;
        }
    }
}
