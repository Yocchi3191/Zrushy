// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;

namespace Zrushy.Core.Domain.Interactions.ValueObject
{
    public class ClothingID
    {
        private readonly string _id;
        public ClothingID(string id) { _id = id ?? throw new ArgumentNullException(nameof(id)); }

        public override bool Equals(object obj)
        {
            if (obj is ClothingID id)
            {
                return _id == id._id;
            }
            return false;
        }

        public override int GetHashCode() => _id.GetHashCode();

        public static bool operator ==(ClothingID left, ClothingID right) => Equals(left, right);
        public static bool operator !=(ClothingID left, ClothingID right) => !Equals(left, right);
    }
}
