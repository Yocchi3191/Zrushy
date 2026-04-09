// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Application.UseCase;

public class InteractPartCommandTest
{
    [Test]
    public void コンストラクタでPartIDを設定できる()
    {
        // Arrange
        var partID = new PartID("chest");

        // Act
        var command = new InteractPartCommand(partID);

        // Assert
        Assert.That(command.PartID, Is.EqualTo(partID));
    }

    [Test]
    public void コンストラクタで異なるPartIDを受け入れる()
    {
        // Arrange
        var partID1 = new PartID("head");
        var partID2 = new PartID("leg");

        // Act
        var command1 = new InteractPartCommand(partID1);
        var command2 = new InteractPartCommand(partID2);

        // Assert
        Assert.That(command1.PartID, Is.EqualTo(partID1));
        Assert.That(command2.PartID, Is.EqualTo(partID2));
    }

    [Test]
    public void Typeを省略するとFingerになる()
    {
        // Arrange
        var partID = new PartID("chest");

        // Act
        var command = new InteractPartCommand(partID);

        // Assert
        Assert.That(command.Type, Is.EqualTo(InteractionType.Finger));
    }

    [Test]
    public void コンストラクタで指定したTypeが設定される()
    {
        // Arrange
        var partID = new PartID("chest");

        // Act & Assert
        Assert.That(new InteractPartCommand(partID, InteractionType.Stroke).Type, Is.EqualTo(InteractionType.Stroke));
        Assert.That(new InteractPartCommand(partID, InteractionType.Press).Type, Is.EqualTo(InteractionType.Press));
        Assert.That(new InteractPartCommand(partID, InteractionType.Tongue).Type, Is.EqualTo(InteractionType.Tongue));
        Assert.That(new InteractPartCommand(partID, InteractionType.Oral).Type, Is.EqualTo(InteractionType.Oral));
        Assert.That(new InteractPartCommand(partID, InteractionType.Lick).Type, Is.EqualTo(InteractionType.Lick));
    }
}
