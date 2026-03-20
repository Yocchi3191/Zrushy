// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace Zrushy.Core.Presentation
{
    /// <summary>
    /// シナリオを進行させることができるインターフェース
    /// </summary>
    public interface IScenarioAdvancable
    {
        bool IsPlaying { get; }
        event Action? OnScenarioStarted;
        event Action? OnScenarioFinished;
        void Next();
    }
}
