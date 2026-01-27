using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Test.Domain.Entity;

public class InteractionTest
{
	[Test]
	public void コンストラクタでPartIDを設定できる()
	{
		// Arrange
		var partID = new PartID("chest");

		// Act
		var interaction = new Interaction(partID);

		// Assert
		Assert.That(interaction.PartID, Is.EqualTo(partID));
	}

	[Test]
	public void コンストラクタで異なるPartIDを受け入れる()
	{
		// Arrange
		var partID1 = new PartID("head");
		var partID2 = new PartID("leg");

		// Act
		var interaction1 = new Interaction(partID1);
		var interaction2 = new Interaction(partID2);

		// Assert
		Assert.That(interaction1.PartID, Is.EqualTo(partID1));
		Assert.That(interaction2.PartID, Is.EqualTo(partID2));
		Assert.That(interaction1.PartID, Is.Not.EqualTo(interaction2.PartID));
	}

}
