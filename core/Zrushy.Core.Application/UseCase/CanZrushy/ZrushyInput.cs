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
        public PartID ZrushyPart { get; init; } // ずらし操作の対象部位

        public ZrushyInput(Vector2 direction, PartID zrushyPart)
        {
            this.Direction = Vector2.Normalize(direction);
            this.ZrushyPart = zrushyPart;
        }
    }
}
