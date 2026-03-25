using System;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Entity
{
    /// <summary>
    /// 1シナリオに含まれる Beat を提供するインターフェース
    /// </summary>
    public interface IBeatProvider
    {
        bool IsCompleted { get; }
        Beat? Current { get; }

        void Advance();
        void Start(ScenarioID id);

        event Action<Beat>? OnBeatReady;
        event Action? OnCompleted;
    }
}
