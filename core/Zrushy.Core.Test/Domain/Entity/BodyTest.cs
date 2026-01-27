using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Test.Domain.Entity;

public class BodyTest
{
	[Test]
	public void コンストラクタで空のボディを作成できる()
	{
		// Act
		var body = new Body();

		// Assert
		Assert.That(body, Is.Not.Null);
	}

	[Test]
	public void AddPartでボディにパーツを追加できる()
	{
		// Arrange
		var body = new Body();
		var partID = new PartID("head");
		var part = new Part(partID, new Pleasure(0), new Development(0), new Affection(0));

		// Act
		body.AddPart(part);
		var retrievedPart = body.GetPart(partID);

		// Assert
		Assert.That(retrievedPart, Is.Not.Null);
		Assert.That(retrievedPart, Is.EqualTo(part));
	}

	[Test]
	public void AddPartで複数のパーツを追加できる()
	{
		// Arrange
		var body = new Body();
		var part1 = new Part(new PartID("head"), new Pleasure(0), new Development(0), new Affection(0));
		var part2 = new Part(new PartID("chest"), new Pleasure(0), new Development(0), new Affection(0));
		var part3 = new Part(new PartID("leg"), new Pleasure(0), new Development(0), new Affection(0));

		// Act
		body.AddPart(part1);
		body.AddPart(part2);
		body.AddPart(part3);

		// Assert
		Assert.That(body.GetPart(new PartID("head")), Is.Not.Null);
		Assert.That(body.GetPart(new PartID("chest")), Is.Not.Null);
		Assert.That(body.GetPart(new PartID("leg")), Is.Not.Null);
	}

	[Test]
	public void GetPartで存在しないパーツはnullを返す()
	{
		// Arrange
		var body = new Body();
		var partID = new PartID("nonexistent");

		// Act
		var part = body.GetPart(partID);

		// Assert
		Assert.That(part, Is.Null);
	}

	[Test]
	public void GetPartで正しいパーツを取得できる()
	{
		// Arrange
		var body = new Body();
		var partID = new PartID("chest");
		var part = new Part(partID, new Pleasure(5), new Development(10), new Affection(15));
		body.AddPart(part);

		// Act
		var retrievedPart = body.GetPart(partID);

		// Assert
		Assert.That(retrievedPart, Is.EqualTo(part));
		Assert.That(retrievedPart.Pleasure.Value, Is.EqualTo(5));
		Assert.That(retrievedPart.Development.Value, Is.EqualTo(10));
		Assert.That(retrievedPart.Affection.Value, Is.EqualTo(15));
	}

	[Test]
	public void Interactで対象パーツのパラメータが更新される()
	{
		// Arrange
		var body = new Body();
		var partID = new PartID("arm");
		var part = new Part(partID, new Pleasure(0), new Development(0), new Affection(0));
		body.AddPart(part);
		var interaction = new Interaction(partID);

		// Act
		body.Interact(interaction);
		var updatedPart = body.GetPart(partID);

		// Assert
		Assert.That(updatedPart!.Pleasure.Value, Is.EqualTo(1));
		Assert.That(updatedPart.Development.Value, Is.EqualTo(1));
		Assert.That(updatedPart.Affection.Value, Is.EqualTo(1));
	}

	[Test]
	public void Interactで他のパーツには影響しない()
	{
		// Arrange
		var body = new Body();
		var partID1 = new PartID("head");
		var partID2 = new PartID("chest");
		var part1 = new Part(partID1, new Pleasure(0), new Development(0), new Affection(0));
		var part2 = new Part(partID2, new Pleasure(0), new Development(0), new Affection(0));
		body.AddPart(part1);
		body.AddPart(part2);

		var interaction = new Interaction(partID1);

		// Act
		body.Interact(interaction);

		// Assert
		var updatedPart1 = body.GetPart(partID1);
		var unchangedPart2 = body.GetPart(partID2);

		Assert.That(updatedPart1!.Pleasure.Value, Is.EqualTo(1));
		Assert.That(unchangedPart2!.Pleasure.Value, Is.EqualTo(0));
	}

	[Test]
	public void Interactで存在しないパーツを指定しても例外を投げない()
	{
		// Arrange
		var body = new Body();
		var interaction = new Interaction(new PartID("nonexistent"));

		// Act & Assert
		Assert.DoesNotThrow(() => body.Interact(interaction));
	}

	[Test]
	public void Interactを複数回実行するとパラメータが累積する()
	{
		// Arrange
		var body = new Body();
		var partID = new PartID("leg");
		var part = new Part(partID, new Pleasure(0), new Development(0), new Affection(0));
		body.AddPart(part);
		var interaction = new Interaction(partID);

		// Act
		body.Interact(interaction);
		body.Interact(interaction);
		body.Interact(interaction);

		// Assert
		var updatedPart = body.GetPart(partID);
		Assert.That(updatedPart!.Pleasure.Value, Is.EqualTo(3));
		Assert.That(updatedPart.Development.Value, Is.EqualTo(3));
		Assert.That(updatedPart.Affection.Value, Is.EqualTo(3));
	}
}
