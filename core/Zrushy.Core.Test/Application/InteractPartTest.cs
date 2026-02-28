using NSubstitute;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Exception;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;
using Zrushy.Core.Infrastructure.EventBus;

namespace Zrushy.Core.Test.Application;

public class InteractPartTest
{
	private static readonly PartConfig _partConfig = new(-2, 0.1f, 0.05f);
	private PartID _partID;
	private Heroin _body;
	private ScenarioID testScenarioID = new ScenarioID("test_scenario");
	IEventBus _eventBus;
	IInteractionHistory _interactionHistory;

	[SetUp]
	public void Setup()
	{
		_partID = new PartID("head");
		_eventBus = new EventBus(new FiredEventLog());
		_body = new Heroin();
		_body.AddPart(new Part(_partID, new Development(0), new Affection(0), _partConfig));

		_interactionHistory = Substitute.For<IInteractionHistory>();
	}

	[Test]
	public void Executeでパラメータが更新される()
	{
		var evt = new StubEvent(canFire: true, priority: 1, testScenarioID);
		var useCase = new InteractPart(_body, new StubEventRepository(evt), _eventBus, _interactionHistory);

		useCase.Execute(new InteractPartCommand(_partID));

		var part = _body.GetPart(_partID);
		// 好感度0・開発度0の部位は不快なので興奮度は負になる
		Assert.That(_body.Arousal.Value, Is.LessThan(0));
		Assert.That(part.Development.Value, Is.EqualTo(1));
		Assert.That(part.Affection.Value, Is.EqualTo(1));
	}

	[Test]
	public void Executeで発火可能なイベントがなければEventBusに何も発行しない()
	{
		IScenarioEvent? published = null;
		_eventBus.OnEventPublished += e => published = e;
		var useCase = new InteractPart(_body, new StubEventRepository(), _eventBus, _interactionHistory);

		useCase.Execute(new InteractPartCommand(_partID));

		Assert.That(published, Is.Null);
	}

	[Test]
	public void Executeで発火可能なイベントをEventBusに発行する()
	{
		IScenarioEvent? published = null;
		_eventBus.OnEventPublished += e => published = e;
		var evt = new StubEvent(canFire: true, priority: 1, testScenarioID);
		var useCase = new InteractPart(_body, new StubEventRepository(evt), _eventBus, _interactionHistory);

		useCase.Execute(new InteractPartCommand(_partID));

		Assert.That(published?.ScenarioToStart, Is.EqualTo(testScenarioID));
	}

	[Test]
	public void Executeで発火不可のイベントはEventBusに発行しない()
	{
		IScenarioEvent? published = null;
		_eventBus.OnEventPublished += e => published = e;
		var evt = new StubEvent(canFire: false, priority: 1, testScenarioID);
		var useCase = new InteractPart(_body, new StubEventRepository(evt), _eventBus, _interactionHistory);

		useCase.Execute(new InteractPartCommand(_partID));

		Assert.That(published, Is.Null);
	}

	[Test]
	public void Executeで複数イベントがあれば最優先のものをEventBusに発行する()
	{
		IScenarioEvent? published = null;
		_eventBus.OnEventPublished += e => published = e;
		var lowScenario = new ScenarioID("low_scenario");
		var highScenario = new ScenarioID("high_scenario");
		var low = new StubEvent(canFire: true, priority: 1, lowScenario);
		var high = new StubEvent(canFire: true, priority: 10, highScenario);
		var useCase = new InteractPart(_body, new StubEventRepository(low, high), _eventBus, _interactionHistory);

		useCase.Execute(new InteractPartCommand(_partID));

		Assert.That(published?.ScenarioToStart, Is.EqualTo(highScenario));
	}

	[Test]
	public void Executeで存在しないパーツは例外を投げる()
	{
		var useCase = new InteractPart(_body, new StubEventRepository(), _eventBus, _interactionHistory);

		Assert.Throws<PartNotFoundException>(() =>
			useCase.Execute(new InteractPartCommand(new PartID("nonexistent"))));
	}

	// --- Stubs ---

	private class StubEvent : IScenarioEvent
	{
		public EventID ID { get; }
		public ScenarioID ScenarioToStart { get; }
		public int Priority { get; }
		private readonly bool _canFire;

		public StubEvent(bool canFire, int priority, ScenarioID scenarioId)
		{
			_canFire = canFire;
			Priority = priority;
			ID = new EventID(scenarioId.Value);
			ScenarioToStart = scenarioId;
		}

		public bool CanFire() => _canFire;
	}

	private class StubEventRepository : IEventRepository
	{
		private readonly IReadOnlyList<IScenarioEvent> _events;

		public StubEventRepository(params IScenarioEvent[] events)
		{
			_events = events;
		}

		public IReadOnlyList<IScenarioEvent> GetEvents(PartID partID) => _events;
	}
}
