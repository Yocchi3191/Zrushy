using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

/// <summary>
/// Arousal値オブジェクトのテスト
/// 境界値 (-100〜+100) と計算ロジックを検証
/// </summary>
public class ArousalTest
{
	// --- 境界値テスト ---

	[Test]
	public void 上限付近での加算は100にクランプされる()
	{
		var result = new Arousal(95) + 10;
		Assert.That(result.Value, Is.EqualTo(Arousal.MAX_VALUE));
	}

	[Test]
	public void 上限ちょうどへの加算は100のまま()
	{
		var result = new Arousal(Arousal.MAX_VALUE) + 1;
		Assert.That(result.Value, Is.EqualTo(Arousal.MAX_VALUE));
	}

	[Test]
	public void 下限付近での減算はマイナス100にクランプされる()
	{
		var result = new Arousal(-95) - 10;
		Assert.That(result.Value, Is.EqualTo(Arousal.MIN_VALUE));
	}

	[Test]
	public void 下限ちょうどへの減算はマイナス100のまま()
	{
		var result = new Arousal(Arousal.MIN_VALUE) - 1;
		Assert.That(result.Value, Is.EqualTo(Arousal.MIN_VALUE));
	}

	// --- 負の値テスト ---

	[Test]
	public void 負の値を保持できる()
	{
		var arousal = new Arousal(-50);
		Assert.That(arousal.Value, Is.EqualTo(-50));
	}

	[Test]
	public void 演算子でゼロを下回り負の値になれる()
	{
		var result = new Arousal(5) - 20;
		Assert.That(result.Value, Is.EqualTo(-15));
	}

	// --- 演算子テスト ---

	[Test]
	public void 加算演算子で値が増加する()
	{
		var result = new Arousal(50) + 10;
		Assert.That(result.Value, Is.EqualTo(60));
	}

	[Test]
	public void 減算演算子で値が減少する()
	{
		var result = new Arousal(50) - 10;
		Assert.That(result.Value, Is.EqualTo(40));
	}

	// --- CalculateGain 計算テスト ---

	[Test]
	public void 開発度0好感度0なら基本値1が加算される()
	{
		var result = new Arousal(0).CalculateGain(new Development(0), new Affection(0));
		Assert.That(result.Value, Is.EqualTo(1));
	}

	[Test]
	public void 開発度ボーナスが加算される()
	{
		// 1 + (10 * 0.1) = 2
		var result = new Arousal(0).CalculateGain(new Development(10), new Affection(0));
		Assert.That(result.Value, Is.EqualTo(2));
	}

	[Test]
	public void 好感度ボーナスが加算される()
	{
		// 1 + (20 * 0.05) = 2
		var result = new Arousal(0).CalculateGain(new Development(0), new Affection(20));
		Assert.That(result.Value, Is.EqualTo(2));
	}

	[Test]
	public void CalculateGainで上限を超えた場合クランプされる()
	{
		var result = new Arousal(99).CalculateGain(new Development(100), new Affection(100));
		Assert.That(result.Value, Is.EqualTo(Arousal.MAX_VALUE));
	}

	// --- ApplyCooldown テスト ---

	[Test]
	public void クールダウンで下限未満になった場合マイナス100にクランプされる()
	{
		// Arousal(-80) - 50(基本減少) = -130 → -100
		var result = new Arousal(-80).ApplyCooldown(new Development(0));
		Assert.That(result.Value, Is.EqualTo(Arousal.MIN_VALUE));
	}

	[Test]
	public void 開発度が高いほどクールダウン量が少なくなる()
	{
		var arousal = new Arousal(0);
		var lowDevResult = arousal.ApplyCooldown(new Development(10));
		var highDevResult = arousal.ApplyCooldown(new Development(90));

		Assert.That(highDevResult.Value, Is.GreaterThan(lowDevResult.Value));
	}
}
