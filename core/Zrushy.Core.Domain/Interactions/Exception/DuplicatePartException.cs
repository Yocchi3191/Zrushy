// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Exception
{
    [Serializable]
    internal class DuplicatePartException : System.Exception
    {
        private PartID _iD;
        private const string DefaultMessage = "同じIDの部位が既に存在しています。";

        public DuplicatePartException(PartID iD) : base(DefaultMessage + " ID: " + iD) => _iD = iD;
    }
}
