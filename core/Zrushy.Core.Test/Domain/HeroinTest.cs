// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Exception;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

public class HeroinTest
{
    private static readonly ClothingID s_id = new ClothingID("shirt");

    [Test]
    public void AddClothingで服を追加できる()
    {
        var heroin = new Heroin(new Arousal(10), new Affection(10));
        heroin.AddClothing(new Clothing(s_id, 10));

        Assert.That(heroin.CanPutOffClothing(s_id), Is.True);
    }

    [Test]
    public void AddClothingで重複IDは例外をスロー()
    {
        var heroin = new Heroin(new Arousal(0), new Affection(0));
        heroin.AddClothing(new Clothing(s_id, 10));

        Assert.Throws<DuplicateClothingException>(() =>
            heroin.AddClothing(new Clothing(s_id, 10)));
    }

    [Test]
    public void CanPutOffClothingでHeroinのパラメータがresistance未満なら脱がせない()
    {
        var heroin = new Heroin(new Arousal(4), new Affection(4));
        heroin.AddClothing(new Clothing(s_id, 10));

        Assert.That(heroin.CanPutOffClothing(s_id), Is.False);
    }

    [Test]
    public void CanPutOffClothingで存在しないIDは例外をスロー()
    {
        var heroin = new Heroin(new Arousal(0), new Affection(0));

        Assert.Throws<ClothingNotFoundException>(() =>
            heroin.CanPutOffClothing(s_id));
    }
}
