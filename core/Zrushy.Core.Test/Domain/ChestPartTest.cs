// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

/// <summary>
/// ChestPart.CalculateArousal のテスト
/// 序盤から快感を稼げること、高開発度で胸イキ可能なことを検証
/// </summary>
public class ChestPartTest
{
    private static readonly PartConfig s_chestConfig = new(3, 0.1f, 0.05f);
    private static readonly PartID s_partID = new PartID("chest");
    private static readonly Interaction s_finger = new Interaction(s_partID, InteractionType.Finger);

    [Test]
    public void 開発度0でも序盤から快感を稼げる()
    {
        ChestPart part = new ChestPart(s_partID, new Development(0), new Affection(0), s_chestConfig);
        Arousal result = part.CalculateArousal(new Arousal(0), s_finger);
        Assert.That(result.Value, Is.GreaterThan(0));
    }

    [Test]
    public void 開発度が高いほどゲインが大きい()
    {
        ChestPart lowDev = new ChestPart(s_partID, new Development(10), new Affection(0), s_chestConfig);
        ChestPart highDev = new ChestPart(s_partID, new Development(90), new Affection(0), s_chestConfig);

        Arousal lowResult = lowDev.CalculateArousal(new Arousal(0), s_finger);
        Arousal highResult = highDev.CalculateArousal(new Arousal(0), s_finger);

        Assert.That(highResult.Value, Is.GreaterThan(lowResult.Value));
    }

    [Test]
    public void 十分な開発度と好感度では絶頂閾値まで到達できる()
    {
        // dev=100, aff=100 → gain=18 なので約6回で絶頂閾値(100)に到達可能
        ChestPart part = new ChestPart(s_partID, new Development(100), new Affection(100), s_chestConfig);
        Arousal result = part.CalculateArousal(new Arousal(82), s_finger);
        Assert.That(result.Value, Is.GreaterThanOrEqualTo(100));
    }

    [Test]
    public void 開発度0では絶頂閾値への到達に多くの回数が必要()
    {
        // dev=0, aff=0 → gain=3 なので33回以上必要
        ChestPart part = new ChestPart(s_partID, new Development(0), new Affection(0), s_chestConfig);
        Arousal result = part.CalculateArousal(new Arousal(0), s_finger);
        Assert.That(result.Value, Is.LessThan(10));
    }
}
