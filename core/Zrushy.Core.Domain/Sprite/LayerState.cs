// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Zrushy.Core.Domain.Sprite
{
    /// <summary>
    /// 部位ごとの状態を表すクラス
    /// ex) 髪型、服装、表情などの状態を表すために使用される
    /// </summary>
    /// <param name="type"></param>
    public record LayerState(string type);
}
