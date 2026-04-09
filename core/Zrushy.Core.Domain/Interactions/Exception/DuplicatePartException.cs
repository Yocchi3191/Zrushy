// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Exception
{
    [Serializable]
    internal class DuplicatePartException : System.Exception
    {
        private readonly PartID _iD;
        private const string DefaultMessage = "同じIDの部位が既に存在しています。";

        public DuplicatePartException(PartID iD) : base(DefaultMessage + " ID: " + iD) => _iD = iD;
    }
}
