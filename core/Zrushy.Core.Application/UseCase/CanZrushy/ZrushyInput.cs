// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Numerics;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Application.UseCase.CanZrushy
{
    /// <summary>
    /// ずらし操作の入力情報
    /// </summary>
    public record ZrushyInput
    {
        public Vector2 Direction { get; init; } // ドラッグ方向の単位ベクトル
        public ClothingID Target { get; init; }

        public ZrushyInput(string clothingID, Vector2 direction)
        {
            Target = new ClothingID(clothingID);
            Direction = Vector2.Normalize(direction);
        }
    }
}
