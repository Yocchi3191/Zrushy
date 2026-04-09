// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Numerics;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Presentation
{
    /// <summary>
    /// 部位への入力を表すクラス
    /// </summary>
    public record PartInput(PartID PartID, InteractionType Type = InteractionType.Finger, Vector2 Direction = default);
}
