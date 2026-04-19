// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Interactions.Exception
{
    [Serializable]
    public class ClothingNotFoundException : System.Exception
    {
        private ClothingID _target;

        public ClothingNotFoundException(ClothingID id) : base($"服が見つかりません ID: {id}")
        {
            _target = id;
        }

    }
}
