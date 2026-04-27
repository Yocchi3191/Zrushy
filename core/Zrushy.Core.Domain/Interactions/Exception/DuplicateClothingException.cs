// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Exception
{
    [Serializable]
    public class DuplicateClothingException : System.Exception
    {
        public DuplicateClothingException(ClothingID id) : base($"同じIDの服が既に存在しています。 ID: {id}") { }
    }
}
