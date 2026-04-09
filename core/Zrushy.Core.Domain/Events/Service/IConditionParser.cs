// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Entity;

namespace Zrushy.Core.Domain.Events.Service
{
    /// <summary>
    /// 条件トークン文字列から ICondition を生成するパーサー
    /// 新しい条件タイプを追加する場合はこのインターフェースを実装して登録する
    /// </summary>
    public interface IConditionParser
    {
        string Type { get; }
        ICondition? Parse(string[] parts);
    }
}
