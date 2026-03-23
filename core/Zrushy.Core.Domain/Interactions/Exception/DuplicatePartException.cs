// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.Serialization;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Exception
{
    [Serializable]
    internal class DuplicatePartException : System.Exception
    {
        private PartID _iD;
        private const string DefaultMessage = "同じIDの部位が既に存在しています。";

        public DuplicatePartException() : base(DefaultMessage) { }

        public DuplicatePartException(PartID iD) : base(DefaultMessage) => _iD = iD;

        public DuplicatePartException(string message) : base(message)
        {
        }

        public DuplicatePartException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicatePartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
