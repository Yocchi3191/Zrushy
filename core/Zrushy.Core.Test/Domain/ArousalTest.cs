// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

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
        Arousal result = new Arousal(95) + 10;
        Assert.That(result.Value, Is.EqualTo(Arousal.MAX_VALUE));
    }

    [Test]
    public void 上限ちょうどへの加算は100のまま()
    {
        Arousal result = new Arousal(Arousal.MAX_VALUE) + 1;
        Assert.That(result.Value, Is.EqualTo(Arousal.MAX_VALUE));
    }

    [Test]
    public void 下限付近での減算はマイナス100にクランプされる()
    {
        Arousal result = new Arousal(-95) - 10;
        Assert.That(result.Value, Is.EqualTo(Arousal.MIN_VALUE));
    }

    [Test]
    public void 下限ちょうどへの減算はマイナス100のまま()
    {
        Arousal result = new Arousal(Arousal.MIN_VALUE) - 1;
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
        Arousal result = new Arousal(5) - 20;
        Assert.That(result.Value, Is.EqualTo(-15));
    }

    // --- 演算子テスト ---

    [Test]
    public void 加算演算子で値が増加する()
    {
        Arousal result = new Arousal(50) + 10;
        Assert.That(result.Value, Is.EqualTo(60));
    }

    [Test]
    public void 減算演算子で値が減少する()
    {
        Arousal result = new Arousal(50) - 10;
        Assert.That(result.Value, Is.EqualTo(40));
    }

    // --- IsAboveThreshold テスト ---

    [Test]
    public void 閾値以上では絶頂状態と判定される()
    {
        Assert.That(new Arousal(100).IsAboveThreshold(100), Is.True);
    }

    [Test]
    public void 閾値未満では絶頂状態と判定されない()
    {
        Assert.That(new Arousal(99).IsAboveThreshold(100), Is.False);
    }

    // --- ApplyCooldown テスト ---

    [Test]
    public void クールダウンを適用すると快感が減少する()
    {
        Arousal result = new Arousal(100).ApplyCooldown(new Development(0));
        Assert.That(result.Value, Is.LessThan(100));
    }

    [Test]
    public void クールダウンで下限未満になった場合マイナス100にクランプされる()
    {
        // Arousal(-80) - 50(基本減少) = -130 → -100
        Arousal result = new Arousal(-80).ApplyCooldown(new Development(0));
        Assert.That(result.Value, Is.EqualTo(Arousal.MIN_VALUE));
    }

    [Test]
    public void 開発度が高いほどクールダウン量が少なくなる()
    {
        var arousal = new Arousal(0);
        Arousal lowDevResult = arousal.ApplyCooldown(new Development(10));
        Arousal highDevResult = arousal.ApplyCooldown(new Development(90));

        Assert.That(highDevResult.Value, Is.GreaterThan(lowDevResult.Value));
    }
}
