// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Entity
{
    /// <summary>
    /// Scenario が再生する Beat を提供するインターフェース
    /// </summary>
    public interface IBeatProvider
    {
        bool IsCompleted { get; }
        Beat? Current { get; }

        void Advance();
        void Start(ScenarioID id);
    }
}
