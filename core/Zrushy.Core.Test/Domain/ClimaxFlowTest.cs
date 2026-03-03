using NSubstitute;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

/// <summary>
/// 絶頂フローのテスト
/// Heroin と Part の結合による Arousal 蓄積・クールダウン自動適用を検証する
/// Arousal 値オブジェクト自体の絶頂判定・クールダウン挙動は ArousalTest で検証
/// </summary>
public class ClimaxFlowTest
{
	private static readonly PartConfig _partConfig = new(-2, 0.1f, 0.05f);
	private Heroin _body;
	private PartID _partID;

	[SetUp]
	public void Setup()
	{
		_body = new Heroin(Substitute.For<IEventEvaluator>());
		_partID = new PartID("test");

		// 開発度50、好感度50の部位を追加
		_body.AddPart(new Part(_partID, new Development(50), new Affection(50), _partConfig));
	}

	[Test]
	public void 快感が蓄積される()
	{
		_body.Interact(new Interaction(_partID));

		Assert.That(_body.Arousal.Value, Is.GreaterThan(0));
	}

	[Test]
	public void 絶頂閾値を超えるとクールダウンが自動適用される()
	{
		// 開発度50・好感度50なので1回あたり約5増加 → 20回前後で絶頂
		bool cooldownOccurred = false;
		for (int i = 0; i < 25; i++)
		{
			int before = _body.Arousal.Value;
			_body.Interact(new Interaction(_partID));

			if (_body.Arousal.Value < before)
			{
				cooldownOccurred = true;
				break;
			}
		}

		Assert.That(cooldownOccurred, Is.True);
	}
}
