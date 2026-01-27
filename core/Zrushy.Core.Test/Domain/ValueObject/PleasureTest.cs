using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Test.Domain.ValueObject;

public class PleasureTest
{
	[Test]
	public void コンストラクタで値を設定できる()
	{
		// Arrange
		int expectedValue = 10;

		// Act
		var pleasure = new Pleasure(expectedValue);

		// Assert
		Assert.That(pleasure.Value, Is.EqualTo(expectedValue));
	}

	[Test]
	public void CalclateGainで値が増加する()
	{
		// Arrange
		var initialPleasure = new Pleasure(5);

		// Act
		var newPleasure = initialPleasure.CalclateGain();

		// Assert
		Assert.That(newPleasure.Value, Is.GreaterThan(initialPleasure.Value));
	}

	[Test]
	public void CalclateGainで1ずつ増加する()
	{
		// Arrange
		var initialPleasure = new Pleasure(10);
		int expectedValue = 11;

		// Act
		var newPleasure = initialPleasure.CalclateGain();

		// Assert
		Assert.That(newPleasure.Value, Is.EqualTo(expectedValue));
	}

	[Test]
	public void CalclateGainは元のオブジェクトを変更しない()
	{
		// Arrange
		var originalPleasure = new Pleasure(7);
		int originalValue = originalPleasure.Value;

		// Act
		var newPleasure = originalPleasure.CalclateGain();

		// Assert
		Assert.That(originalPleasure.Value, Is.EqualTo(originalValue));
		Assert.That(newPleasure, Is.Not.SameAs(originalPleasure));
	}

	[Test]
	public void コンストラクタでゼロ値を受け入れる()
	{
		// Arrange & Act
		var pleasure = new Pleasure(0);

		// Assert
		Assert.That(pleasure.Value, Is.EqualTo(0));
	}

	[Test]
	public void コンストラクタで負の値を受け入れる()
	{
		// Arrange & Act
		var pleasure = new Pleasure(-5);

		// Assert
		Assert.That(pleasure.Value, Is.EqualTo(-5));
	}
}
