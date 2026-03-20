// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Domain.Scenarios.Entity
{
    /// <summary>
    /// イベント発火時に再生するシナリオ
    /// </summary>
    public class Scenario
    {
        public ScenarioID ID { get; }
        private readonly IBeatProvider beatProvider;

        public Scenario(ScenarioID id, IBeatProvider beatProvider)
        {
            this.ID = id;
            this.beatProvider = beatProvider;
            this.beatProvider.Start(id);
        }

        public Beat? Current => this.beatProvider.Current;
        public void Next() => this.beatProvider.Advance();
        public bool IsFinished => this.beatProvider.IsCompleted;
    }
}
