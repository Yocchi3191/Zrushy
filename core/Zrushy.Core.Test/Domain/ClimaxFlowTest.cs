// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

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
    private static readonly PartConfig s_partConfig = new(-2, 0.1f, 0.05f);
    private Heroin _body;
    private PartID _partID;

    [SetUp]
    public void Setup()
    {
        _body = new Heroin(new Arousal(0), new Affection(50));
        _partID = new PartID("test");

        _body.AddPart(new Part(_partID, new Development(50), s_partConfig));
    }

    [Test]
    public void 快感が蓄積される()
    {
        _body.Interact(new Interaction(_partID));

        Assert.That(_body.Arousal.Value, Is.GreaterThan(0));
    }
}
