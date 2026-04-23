// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Numerics;
using NSubstitute;
using NUnit.Framework;
using Zrushy.Core.Application.UseCase.CanZrushy;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Application.UseCase;

public class ZrushyClothingTest
{
    [Test]
    public void CanPutOffClothingがtrueのときtrueを返す()
    {
        var heroin = CreateHeroin(canPutOff: true);
        var evaluator = Substitute.For<IClothingEventEvaluator>();
        var useCase = (IZrushyClothing)new ZrushyClothing(heroin, evaluator);

        bool result = useCase.Execute(new ZrushyInput("hoodie", new Vector2(1, 0)));

        Assert.That(result, Is.True);
    }

    [Test]
    public void CanPutOffClothingがfalseのときfalseを返す()
    {
        var heroin = CreateHeroin(canPutOff: false);
        var evaluator = Substitute.For<IClothingEventEvaluator>();
        var useCase = (IZrushyClothing)new ZrushyClothing(heroin, evaluator);

        bool result = useCase.Execute(new ZrushyInput("hoodie", new Vector2(1, 0)));

        Assert.That(result, Is.False);
    }

    [Test]
    public void Executeが呼ばれたらEvaluateを呼ぶ()
    {
        var heroin = CreateHeroin(canPutOff: true);
        var evaluator = Substitute.For<IClothingEventEvaluator>();
        var useCase = (IZrushyClothing)new ZrushyClothing(heroin, evaluator);
        var input = new ZrushyInput("hoodie", new Vector2(1, 0));

        useCase.Execute(input);

        evaluator.Received(1).Evaluate(input.Target, true);
    }

    [Test]
    public void 失敗時もEvaluateを呼ぶ()
    {
        var heroin = CreateHeroin(canPutOff: false);
        var evaluator = Substitute.For<IClothingEventEvaluator>();
        var useCase = (IZrushyClothing)new ZrushyClothing(heroin, evaluator);
        var input = new ZrushyInput("hoodie", new Vector2(1, 0));

        useCase.Execute(input);

        evaluator.Received(1).Evaluate(input.Target, false);
    }

    private static Heroin CreateHeroin(bool canPutOff)
    {
        var heroin = new Heroin(new Arousal(0), new Affection(0));
        int resistance = canPutOff ? 0 : 100;
        heroin.AddClothing(new Clothing(new ClothingID("hoodie"), resistance));
        return heroin;
    }
}
