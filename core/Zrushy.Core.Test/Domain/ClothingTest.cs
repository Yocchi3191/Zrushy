// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

public class ClothingTest
{
    [Test]
    public void IDがnullのとき例外をスロー()
    {
        Assert.Throws<ArgumentNullException>(() => new Clothing(null, 10));
    }

    [Test]
    public void resistanceが負のとき例外をスロー()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Clothing(new ClothingID("test_cloth"), -1));
    }

    [Test]
    public void affectionとarousalの合計がresistanceに満たないとき脱がせない()
    {
        var clothing = new Clothing(new ClothingID("test_cloth"),10);
        Assert.That(clothing.CanPutOff(new Affection(4), new Arousal(5)), Is.False);
    }

    [Test]
    public void affectionとarousalの合計がresistanceと等しいとき脱がせる()
    {
        var clothing = new Clothing(new ClothingID("test_cloth"),10);
        Assert.That(clothing.CanPutOff(new Affection(5), new Arousal(5)), Is.True);
    }

    [Test]
    public void affectionとarousalの合計がresistanceを超えるとき脱がせる()
    {
        var clothing = new Clothing(new ClothingID("test_cloth"),10);
        Assert.That(clothing.CanPutOff(new Affection(8), new Arousal(5)), Is.True);
    }

    [Test]
    public void resistanceが0のとき常に脱がせる()
    {
        var clothing = new Clothing(new ClothingID("test_cloth"),0);
        Assert.That(clothing.CanPutOff(new Affection(0), new Arousal(0)), Is.True);
    }
}
