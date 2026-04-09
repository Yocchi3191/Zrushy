// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

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
