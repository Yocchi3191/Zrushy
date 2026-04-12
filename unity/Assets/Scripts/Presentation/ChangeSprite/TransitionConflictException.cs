// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;

namespace Zrushy.Core.Presentation.Unity
{
    [Serializable]
    internal class TransitionConflictException : Exception
    {
        private StateTransition _transition;
        private const string DefaultMessage = "衝突する遷移が登録されました";
        public TransitionConflictException(StateTransition transition)
            : base($"{DefaultMessage}: fromState={transition.fromState?.name}, direction={transition.requiredDirection}")
        {
            _transition = transition;
        }
    }
}
