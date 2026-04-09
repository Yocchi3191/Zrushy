// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Numerics;

namespace Zrushy.Core.Application.UseCase.CanZrushy
{
    /// <summary>
    /// ずらし操作の入力情報
    /// </summary>
    public record ZrushyInput
    {
        public Vector2 Direction { get; init; } // ドラッグ方向の単位ベクトル

        public ZrushyInput(Vector2 direction)
        {
            Direction = Vector2.Normalize(direction);
        }
    }
}
