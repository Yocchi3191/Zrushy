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

public class ClothingEventEvaluatorTest
{
    private IZrushyHistory _history;
    private EventBus _eventBus;
    private ClothingID _clothingID;
    private ScenarioID _testScenarioID;

    [SetUp]
    public void Setup()
    {
        _clothingID = new ClothingID("hoodie");
        _testScenarioID = new ScenarioID("test_scenario");
        _history = Substitute.For<IZrushyHistory>();
        _eventBus = new EventBus(new FiredEventLog());
    }

    [Test]
    public void 発火可能なイベントがなければEventBusに何も発行しない()
    {
        EventID? published = null;
        _eventBus.OnEventPublished += e => published = e;
        var evaluator = new ClothingEventEvaluator(_history, new StubRepository(), _eventBus);

        evaluator.Evaluate(_clothingID, isSuccess: true);

        Assert.That(published, Is.Null);
    }

    [Test]
    public void 発火可能なイベントをEventBusに発行する()
    {
        EventID? published = null;
        _eventBus.OnEventPublished += e => published = e;
        var evt = new StubEvent(canFire: true, priority: 1, _testScenarioID);
        var evaluator = new ClothingEventEvaluator(_history, new StubRepository(evt), _eventBus);

        evaluator.Evaluate(_clothingID, isSuccess: true);

        Assert.That(published, Is.EqualTo(evt.ID));
    }

    [Test]
    public void 発火不可のイベントはEventBusに発行しない()
    {
        EventID? published = null;
        _eventBus.OnEventPublished += e => published = e;
        var evt = new StubEvent(canFire: false, priority: 1, _testScenarioID);
        var evaluator = new ClothingEventEvaluator(_history, new StubRepository(evt), _eventBus);

        evaluator.Evaluate(_clothingID, isSuccess: true);

        Assert.That(published, Is.Null);
    }

    [Test]
    public void 複数イベントがあれば最優先のものをEventBusに発行する()
    {
        EventID? published = null;
        _eventBus.OnEventPublished += e => published = e;
        var low = new StubEvent(canFire: true, priority: 1, new ScenarioID("low"));
        var high = new StubEvent(canFire: true, priority: 10, new ScenarioID("high"));
        var evaluator = new ClothingEventEvaluator(_history, new StubRepository(low, high), _eventBus);

        evaluator.Evaluate(_clothingID, isSuccess: true);

        Assert.That(published, Is.EqualTo(high.ID));
    }

    [Test]
    public void ずらし履歴が記録される()
    {
        var evaluator = new ClothingEventEvaluator(_history, new StubRepository(), _eventBus);

        evaluator.Evaluate(_clothingID, isSuccess: true);

        _history.Received(1).Record(_clothingID, true);
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

    private class StubRepository(params IScenarioEvent[] events) : IClothingEventRepository
    {
        private readonly IReadOnlyList<IScenarioEvent> _events = events;
        public IReadOnlyList<IScenarioEvent> GetEvents(ClothingID clothingID, bool isSuccess) => _events;
    }
}
