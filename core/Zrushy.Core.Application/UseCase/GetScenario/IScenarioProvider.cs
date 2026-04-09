// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Application.UseCase.GetScenario
{
    /// <summary>
    /// 発火したイベントに対応するシナリオを提供するインターフェース
    /// </summary>
    public interface IScenarioProvider
    {
        ScenarioInfo[] GetPlayableScenarios(EventID triggeredEvent);
    }
}
