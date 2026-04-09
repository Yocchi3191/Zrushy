// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using NSubstitute;
using Zrushy.Core.Application;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Test.Application;

public class EventEvaluatorTest
{
    private IInteractionHistory _history;
    private EventBus _eventBus;
    private PartID _partID;
    private ScenarioID _testScenarioID;

    [SetUp]
    public void Setup()
    {
        _partID = new PartID("head");
        _testScenarioID = new ScenarioID("test_scenario");
        _history = Substitute.For<IInteractionHistory>();
        _eventBus = new EventBus(new FiredEventLog());
    }

    [Test]
    public void 発火可能なイベントがなければEventBusに何も発行しない()
    {
        EventID? published = null;
        _eventBus.OnEventPublished += e => published = e;
        var evaluator = new EventEvaluator(_history, new StubEventRepository(), _eventBus);

        evaluator.Evaluate(new Interaction(_partID));

        Assert.That(published, Is.Null);
    }

    [Test]
    public void 発火可能なイベントをEventBusに発行する()
    {
        EventID? published = null;
        _eventBus.OnEventPublished += e => published = e;
        var evt = new StubEvent(canFire: true, priority: 1, _testScenarioID);
        var evaluator = new EventEvaluator(_history, new StubEventRepository(evt), _eventBus);

        evaluator.Evaluate(new Interaction(_partID));

        Assert.That(published, Is.EqualTo(evt.ID));
    }

    [Test]
    public void 発火不可のイベントはEventBusに発行しない()
    {
        EventID? published = null;
        _eventBus.OnEventPublished += e => published = e;
        var evt = new StubEvent(canFire: false, priority: 1, _testScenarioID);
        var evaluator = new EventEvaluator(_history, new StubEventRepository(evt), _eventBus);

        evaluator.Evaluate(new Interaction(_partID));

        Assert.That(published, Is.Null);
    }

    [Test]
    public void 複数イベントがあれば最優先のものをEventBusに発行する()
    {
        EventID? published = null;
        _eventBus.OnEventPublished += e => published = e;
        var lowScenario = new ScenarioID("low_scenario");
        var highScenario = new ScenarioID("high_scenario");
        var low = new StubEvent(canFire: true, priority: 1, lowScenario);
        var high = new StubEvent(canFire: true, priority: 10, highScenario);
        var evaluator = new EventEvaluator(_history, new StubEventRepository(low, high), _eventBus);

        evaluator.Evaluate(new Interaction(_partID));

        Assert.That(published, Is.EqualTo(high.ID));
    }

    [Test]
    public void インタラクション履歴が記録される()
    {
        var evaluator = new EventEvaluator(_history, new StubEventRepository(), _eventBus);
        var interaction = new Interaction(_partID);

        evaluator.Evaluate(interaction);

        _history.Received(1).Record(interaction);
    }

    // --- Stubs ---

    private class StubEvent(bool canFire, int priority, ScenarioID scenarioId) : IScenarioEvent
    {
        public EventID ID { get; } = new EventID(scenarioId.Value);
        public ScenarioID ScenarioToStart { get; } = scenarioId;
        public int Priority { get; } = priority;
        private readonly bool _canFire = canFire;

        public bool CanFire() => _canFire;
    }

    private class StubEventRepository(params IScenarioEvent[] events) : IEventRepository
    {
        private readonly IReadOnlyList<IScenarioEvent> _events = events;

        public IReadOnlyList<IScenarioEvent> GetEvents(PartID partID) => _events;
        public IReadOnlyList<IScenarioEvent> GetGlobalEvents() => Array.Empty<IScenarioEvent>();
    }
}
