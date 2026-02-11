using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Infrastructure.EventBus;

namespace Zrushy.Core.Test.Domain;

/// <summary>
/// 絶頂フローのテスト
/// 快感の蓄積、絶頂判定、クールダウンの動作を検証
/// </summary>
public class ClimaxFlowTest
{
	private static readonly PartConfig _partConfig = new(-2, 0.1f, 0.05f);
	private Heroin _body;
	private IEventBus _eventBus;
	private PartID _partID;

	[SetUp]
	public void Setup()
	{
		_eventBus = new EventBus(new FiredEventLog());
		_body = new Heroin(_eventBus);
		_partID = new PartID("test");

		// 開発度50、好感度50の部位を追加
		_body.AddPart(new Part(_partID, new Development(50), new Affection(50), _partConfig));
	}

	[Test]
	public void 快感が閾値を超えると絶頂イベントが発火する()
	{
		// Arrange
		bool climaxFired = false;
		_eventBus.OnEventPublished += (e) =>
		{
			if (e.Priority >= 1000)
			{
				climaxFired = true;
			}
		};

		// Act: Interact を繰り返して快感を蓄積
		// 開発度50、好感度50なので、1回あたり約5増加 (-2 + 5 + 2)
		// 100 / 5 ≒ 20回で絶頂
		for (int i = 0; i < 25; i++)
		{
			_body.Interact(new Interaction(_partID));
		}

		// Assert
		Assert.That(climaxFired, Is.True);
	}

	[Test]
	public void 絶頂後はクールダウンが適用される()
	{
		// 絶頂が発火するまでInteractし続け、発火直前との比較でクールダウンを検証
		for (int i = 0; i < 100; i++)
		{
			int arousalBefore = _body.Arousal.Value;
			_body.Interact(new Interaction(_partID));

			if (_body.Arousal.Value < arousalBefore)
			{
				// クールダウン発生を確認
				Assert.That(_body.Arousal.Value, Is.LessThan(arousalBefore));
				return;
			}
		}

		Assert.Fail("100回以内に絶頂が発火しなかった");
	}

	[Test]
	public void 開発度が高いほどクールダウンが緩やかになる()
	{
		// Arrange: 開発度の異なる2つのケースを比較
		var lowDevBody = new Heroin(_eventBus);
		var lowDevPartID = new PartID("low_dev");
		lowDevBody.AddPart(new Part(lowDevPartID, new Development(10), new Affection(50), _partConfig));

		var highDevBody = new Heroin(_eventBus);
		var highDevPartID = new PartID("high_dev");
		highDevBody.AddPart(new Part(highDevPartID, new Development(90), new Affection(50), _partConfig));

		// Act: 両方を絶頂まで持っていく
		for (int i = 0; i < 30; i++)
		{
			lowDevBody.Interact(new Interaction(lowDevPartID));
			highDevBody.Interact(new Interaction(highDevPartID));
		}

		// Assert: 開発度が高い方が快感の残存量が多い
		Assert.That(highDevBody.Arousal.Value, Is.GreaterThan(lowDevBody.Arousal.Value));
	}

	[Test]
	public void 絶頂イベントは高優先度を持つ()
	{
		// Arrange
		IScenarioEvent? firedEvent = null;
		_eventBus.OnEventPublished += (e) =>
		{
			if (e.Priority >= 1000)
			{
				firedEvent = e;
			}
		};

		// Act: 絶頂まで持っていく
		for (int i = 0; i < 20; i++)
		{
			_body.Interact(new Interaction(_partID));
		}

		// Assert
		Assert.That(firedEvent, Is.Not.Null);
		Assert.That(firedEvent!.Priority, Is.EqualTo(1000));
	}

	[Test]
	public void 快感が閾値未満では絶頂イベントは発火しない()
	{
		// Arrange
		bool climaxFired = false;
		_eventBus.OnEventPublished += (e) =>
		{
			if (e.Priority >= 1000)
			{
				climaxFired = true;
			}
		};

		// Act: 閾値に届かない程度に Interact
		for (int i = 0; i < 5; i++)
		{
			_body.Interact(new Interaction(_partID));
		}

		// Assert
		Assert.That(climaxFired, Is.False);
	}
}
