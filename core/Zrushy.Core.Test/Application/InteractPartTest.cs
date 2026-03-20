// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NSubstitute;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Exception;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Application;

public class InteractPartTest
{
    private static readonly PartConfig _partConfig = new(-2, 0.1f, 0.05f);
    private PartID _partID;
    private Heroin _heroin;

    private InteractPart useCase;

    [SetUp]
    public void Setup()
    {
        _partID = new PartID("head");
        _heroin = new Heroin();
        _heroin.AddPart(new Part(_partID, new Development(0), new Affection(0), _partConfig));

        useCase = new InteractPart(_heroin, Substitute.For<IEventEvaluator>());
    }

    [Test]
    public void Executeでパラメータが更新される()
    {
        useCase.Execute(new InteractPartCommand(_partID));

        var part = _heroin.GetPart(_partID);
        // 好感度0・開発度0の部位は不快なので興奮度は負になる
        Assert.That(_heroin.Arousal.Value, Is.LessThan(0));
        Assert.That(part.Development.Value, Is.EqualTo(1));
        Assert.That(part.Affection.Value, Is.EqualTo(1));
    }

    [Test]
    public void Executeで存在しないパーツは例外を投げる()
    {
        Assert.Throws<PartNotFoundException>(() =>
            useCase.Execute(new InteractPartCommand(new PartID("nonexistent"))));
    }

    [Test]
    public void 絶頂閾値を超えるとクールダウンが自動適用される()
    {
        // 開発度50・好感度50なので1回あたり約5増加 → 20回前後で絶頂
        var bodyPartID = new PartID("body");
        _heroin.AddPart(new Part(bodyPartID, new Development(50), new Affection(50), _partConfig));

        bool cooldownOccurred = false;
        for (int i = 0; i < 100; i++)
        {
            int before = _heroin.Arousal.Value;
            useCase.Execute(new InteractPartCommand(bodyPartID));

            if (_heroin.Arousal.Value < before)
            {
                cooldownOccurred = true;
                break;
            }
        }

        Assert.That(cooldownOccurred, Is.True);
    }
}
