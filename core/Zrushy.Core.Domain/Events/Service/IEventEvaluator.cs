// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Domain.Events.Service
{
    /// <summary>
    /// インタラクションに基づいてイベント発火条件を評価し、
    /// 発火対象イベントをイベントバスに送信するサービス
    /// </summary>
    public interface IEventEvaluator
    {
        void Evaluate(Interaction interaction);
    }
}
