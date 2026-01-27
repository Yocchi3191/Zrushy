using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;
using Zrushy.Core.Infrastructure.Repository;

namespace Zrushy.Core.Test.Infrastructure.Repository;

public class EventRepositoryTest
{
	private IEventRepository _repository;

	[SetUp]
	public void Setup()
	{
		_repository = new EventRepository();
	}

	[Test]
	public void GetEventで現在の実装ではnullを返す()
	{
		// Arrange
		var partID = new PartID("chest");
		var pleasure = new Pleasure(10);
		var development = new Development(20);
		var affection = new Affection(30);

		// Act
		var evt = _repository.GetEvent(partID, pleasure, development, affection);

		// Assert
		Assert.That(evt, Is.Null);
	}

	[Test]
	public void GetEventで異なるパラメータでもnullを返す()
	{
		// Arrange
		var partID1 = new PartID("head");
		var partID2 = new PartID("leg");
		var pleasure1 = new Pleasure(0);
		var pleasure2 = new Pleasure(100);

		// Act
		var evt1 = _repository.GetEvent(partID1, pleasure1, new Development(0), new Affection(0));
		var evt2 = _repository.GetEvent(partID2, pleasure2, new Development(50), new Affection(75));

		// Assert
		Assert.That(evt1, Is.Null);
		Assert.That(evt2, Is.Null);
	}

	[Test]
	public void GetEventでゼロパラメータの場合もnullを返す()
	{
		// Arrange
		var partID = new PartID("test_part");
		var pleasure = new Pleasure(0);
		var development = new Development(0);
		var affection = new Affection(0);

		// Act
		var evt = _repository.GetEvent(partID, pleasure, development, affection);

		// Assert
		Assert.That(evt, Is.Null);
	}

	[Test]
	public void GetEventで例外を投げない()
	{
		// Arrange
		var partID = new PartID("any_part");
		var pleasure = new Pleasure(5);
		var development = new Development(10);
		var affection = new Affection(15);

		// Act & Assert
		Assert.DoesNotThrow(() => _repository.GetEvent(partID, pleasure, development, affection));
	}
}
