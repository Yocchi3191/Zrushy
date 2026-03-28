// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
