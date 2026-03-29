// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Service
{
    /// <summary>
    /// 複数のシナリオから、どのシナリオをプレイするか選択するクラス
    /// </summary>
    public class ScenarioSelector
    {
        public ScenarioInfo SelectScenarioInfoToPlay(ScenarioInfo[] scenarios)
        {
            if (scenarios == null || scenarios.Length == 0)
            {
                throw new ScenarioNotFoundException("Scenarios array cannot be null or empty.");
            }

            // TODO: シナリオ選択のロジックを実装する
            return scenarios[0];
        }
    }
}
