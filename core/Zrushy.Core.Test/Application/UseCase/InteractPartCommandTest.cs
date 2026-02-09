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
}
