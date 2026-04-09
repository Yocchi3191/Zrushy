// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

namespace Zrushy.Core.Domain.Sprite
{
    /// <summary>
    /// 部位ごとの状態を表すクラス
    /// ex) 髪型、服装、表情などの状態を表すために使用される
    /// </summary>
    /// <param name="type"></param>
    public record LayerState(string type);
}
