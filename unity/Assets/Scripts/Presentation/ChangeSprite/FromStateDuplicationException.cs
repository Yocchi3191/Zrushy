// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using System.Runtime.Serialization;

namespace Zrushy.Core.Presentation.Unity
{
    [Serializable]
    internal class FromStateDuplicationException : Exception
    {
        private StateTransition _transition;

        public FromStateDuplicationException()
        {
        }

        public FromStateDuplicationException(StateTransition transition) => _transition = transition;

        public FromStateDuplicationException(string message) : base(message)
        {
        }

        public FromStateDuplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FromStateDuplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}