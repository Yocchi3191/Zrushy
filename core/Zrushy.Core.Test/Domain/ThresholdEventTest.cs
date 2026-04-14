// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

public class ThresholdEventTest
{
    private PartID _partID;

    [SetUp]
    public void Setup()
    {
        _partID = new PartID("head");
    }

    [Test]
    public void 快感が閾値範囲内なら発火する()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(new Arousal(10), new Arousal(100), () => new Arousal(50)));

        Assert.That(condition.CanFire(), Is.True);
    }

    [Test]
    public void 快感が閾値範囲外なら発火しない()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(new Arousal(10), new Arousal(100), () => new Arousal(5)));

        Assert.That(condition.CanFire(), Is.False);
    }

    [Test]
    public void 指定していないパラメータは無視される()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(new Arousal(10), new Arousal(100), () => new Arousal(50)));

        Assert.That(condition.CanFire(), Is.True);
    }

    [Test]
    public void 閾値なしなら常に発火する()
    {
        var condition = new ThresholdCondition();

        Assert.That(condition.CanFire(), Is.True);
    }

    [Test]
    public void Minだけ指定で下限のみ判定できる()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(new Arousal(10), null, () => new Arousal(50)));

        Assert.That(condition.CanFire(), Is.True);
    }

    [Test]
    public void Minだけ指定で下限未満なら発火しない()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(new Arousal(10), null, () => new Arousal(5)));

        Assert.That(condition.CanFire(), Is.False);
    }

    [Test]
    public void Maxだけ指定で上限のみ判定できる()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(null, new Arousal(100), () => new Arousal(50)));

        Assert.That(condition.CanFire(), Is.True);
    }

    [Test]
    public void Maxだけ指定で上限超過なら発火しない()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(null, new Arousal(100), () => new Arousal(150)));

        Assert.That(condition.CanFire(), Is.False);
    }

    [Test]
    public void 複数パラメータの閾値を同時に判定できる()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(new Arousal(10), new Arousal(100), () => new Arousal(50)),
            new Threshold<Development>(new Development(10), new Development(50), () => new Development(30)));

        Assert.That(condition.CanFire(), Is.True);
    }

    [Test]
    public void 複数パラメータのうち1つでも範囲外なら発火しない()
    {
        var condition = new ThresholdCondition(
            new Threshold<Arousal>(new Arousal(10), new Arousal(100), () => new Arousal(50)),
            new Threshold<Development>(new Development(10), new Development(50), () => new Development(60)));

        Assert.That(condition.CanFire(), Is.False);
    }
}
