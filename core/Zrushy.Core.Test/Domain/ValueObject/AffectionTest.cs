using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Test.Domain.ValueObject;

public class AffectionTest
{
	[Test]
	public void コンストラクタで値を設定できる()
	{
		// Arrange
		int expectedValue = 30;

		// Act
		var affection = new Affection(expectedValue);

		// Assert
		Assert.That(affection.Value, Is.EqualTo(expectedValue));
	}

	[Test]
	public void CalclateGainで値が増加する()
	{
		// Arrange
		var initialAffection = new Affection(5);

		// Act
		var newAffection = initialAffection.CalclateGain();

		// Assert
		Assert.That(newAffection.Value, Is.GreaterThan(initialAffection.Value));
	}

	[Test]
	public void CalclateGainで値が1増える()
	{
		// Arrange
		var initialAffection = new Affection(10);
		int expectedValue = 11;

		// Act
		var newAffection = initialAffection.CalclateGain();

		// Assert
		Assert.That(newAffection.Value, Is.EqualTo(expectedValue));
	}

	[Test]
	public void CalclateGainで元のオブジェクトは変更されない()
	{
		// Arrange
		var originalAffection = new Affection(7);
		int originalValue = originalAffection.Value;

		// Act
		var newAffection = originalAffection.CalclateGain();

		// Assert
		Assert.That(originalAffection.Value, Is.EqualTo(originalValue));
		Assert.That(newAffection, Is.Not.SameAs(originalAffection));
	}

	[Test]
	public void コンストラクタでゼロ値を受け入れる()
	{
		// Arrange & Act
		var affection = new Affection(0);

		// Assert
		Assert.That(affection.Value, Is.EqualTo(0));
	}
}
