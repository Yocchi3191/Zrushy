using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

/// <summary>
/// Part.CalculateArousal のテスト
/// 計算式: -2 + (開発度 * 0.1) + (好感度 * 0.05)
/// 開発度0・好感度0は不快、開発度20以上で快感に転じる
/// </summary>
public class PartTest
{
	private static readonly PartConfig _partConfig = new(-2, 0.1f, 0.05f);
	private static readonly Interaction _fingerInteraction = new Interaction(new PartID("test"), InteractionType.Finger);

	[Test]
	public void 開発度0好感度0は不快になる()
	{
		var part = new Part(new PartID("test"), new Development(0), new Affection(0), _partConfig);
		var result = part.CalculateArousal(new Arousal(0), _fingerInteraction);
		Assert.That(result.Value, Is.LessThan(0));
	}

	[Test]
	public void 開発度20以上で快感に転じる()
	{
		// -2 + (30 * 0.1) + 0 = 1
		var part = new Part(new PartID("test"), new Development(30), new Affection(0), _partConfig);
		var result = part.CalculateArousal(new Arousal(0), _fingerInteraction);
		Assert.That(result.Value, Is.GreaterThan(0));
	}

	[Test]
	public void ベースが負でも計算できる()
	{
		// -2 + 5 + 2 = 5, base=-10 → -10+5 = -5
		var part = new Part(new PartID("test"), new Development(50), new Affection(50), _partConfig);
		var result = part.CalculateArousal(new Arousal(-10), _fingerInteraction);
		Assert.That(result.Value, Is.EqualTo(-5));
	}

	[Test]
	public void CalculateArousalで上限を超えた場合クランプされる()
	{
		// -2 + (100*0.1) + (100*0.05) = 13, base=99 → 99+13=112 → 100
		var part = new Part(new PartID("test"), new Development(100), new Affection(100), _partConfig);
		var result = part.CalculateArousal(new Arousal(99), _fingerInteraction);
		Assert.That(result.Value, Is.EqualTo(Arousal.MAX_VALUE));
	}
}
