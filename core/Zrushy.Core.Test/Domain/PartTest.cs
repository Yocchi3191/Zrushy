using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Test.Domain;

public class PartTest
{
	private PartID _testPartID;

	[SetUp]
	public void Setup()
	{
		_testPartID = new PartID("test_part");
	}

	[Test]
	public void コンストラクタで全てのプロパティを設定できる()
	{
		// Arrange
		var pleasure = new Pleasure(10);
		var development = new Development(20);
		var affection = new Affection(30);

		// Act
		var part = new Part(_testPartID, pleasure, development, affection);

		// Assert
		Assert.That(part.ID, Is.EqualTo(_testPartID));
		Assert.That(part.Pleasure.Value, Is.EqualTo(10));
		Assert.That(part.Development.Value, Is.EqualTo(20));
		Assert.That(part.Affection.Value, Is.EqualTo(30));
	}

	[Test]
	public void Interactで快楽値が増加する()
	{
		// Arrange
		var part = new Part(_testPartID, new Pleasure(5), new Development(10), new Affection(15));
		var interaction = new Interaction(_testPartID);
		int initialPleasure = part.Pleasure.Value;

		// Act
		part.Interact(interaction);

		// Assert
		Assert.That(part.Pleasure.Value, Is.GreaterThan(initialPleasure));
	}

	[Test]
	public void Interactで発達度が増加する()
	{
		// Arrange
		var part = new Part(_testPartID, new Pleasure(5), new Development(10), new Affection(15));
		var interaction = new Interaction(_testPartID);
		int initialDevelopment = part.Development.Value;

		// Act
		part.Interact(interaction);

		// Assert
		Assert.That(part.Development.Value, Is.GreaterThan(initialDevelopment));
	}

	[Test]
	public void Interactで好感度が増加する()
	{
		// Arrange
		var part = new Part(_testPartID, new Pleasure(5), new Development(10), new Affection(15));
		var interaction = new Interaction(_testPartID);
		int initialAffection = part.Affection.Value;

		// Act
		part.Interact(interaction);

		// Assert
		Assert.That(part.Affection.Value, Is.GreaterThan(initialAffection));
	}

	[Test]
	public void Interactで全てのパラメータが1ずつ増加する()
	{
		// Arrange
		var part = new Part(_testPartID, new Pleasure(5), new Development(10), new Affection(15));
		var interaction = new Interaction(_testPartID);

		// Act
		part.Interact(interaction);

		// Assert
		Assert.That(part.Pleasure.Value, Is.EqualTo(6));
		Assert.That(part.Development.Value, Is.EqualTo(11));
		Assert.That(part.Affection.Value, Is.EqualTo(16));
	}

	[Test]
	public void Interactを複数回実行するとパラメータが累積する()
	{
		// Arrange
		var part = new Part(_testPartID, new Pleasure(0), new Development(0), new Affection(0));
		var interaction = new Interaction(_testPartID);

		// Act
		part.Interact(interaction);
		part.Interact(interaction);
		part.Interact(interaction);

		// Assert
		Assert.That(part.Pleasure.Value, Is.EqualTo(3));
		Assert.That(part.Development.Value, Is.EqualTo(3));
		Assert.That(part.Affection.Value, Is.EqualTo(3));
	}
}
