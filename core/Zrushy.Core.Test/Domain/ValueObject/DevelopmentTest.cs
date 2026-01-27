using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Test.Domain.ValueObject;

public class DevelopmentTest
{
	[Test]
	public void コンストラクタで値を設定できる()
	{
		// Arrange
		int expectedValue = 20;

		// Act
		var development = new Development(expectedValue);

		// Assert
		Assert.That(development.Value, Is.EqualTo(expectedValue));
	}

	[Test]
	public void CalclateGainで値が増加する()
	{
		// Arrange
		var initialDevelopment = new Development(5);

		// Act
		var newDevelopment = initialDevelopment.CalclateGain();

		// Assert
		Assert.That(newDevelopment.Value, Is.GreaterThan(initialDevelopment.Value));
	}

	[Test]
	public void CalclateGainで値が1増える()
	{
		// Arrange
		var initialDevelopment = new Development(10);
		int expectedValue = 11;

		// Act
		var newDevelopment = initialDevelopment.CalclateGain();

		// Assert
		Assert.That(newDevelopment.Value, Is.EqualTo(expectedValue));
	}

	[Test]
	public void CalclateGainで元のオブジェクトは変更されない()
	{
		// Arrange
		var originalDevelopment = new Development(7);
		int originalValue = originalDevelopment.Value;

		// Act
		var newDevelopment = originalDevelopment.CalclateGain();

		// Assert
		Assert.That(originalDevelopment.Value, Is.EqualTo(originalValue));
		Assert.That(newDevelopment, Is.Not.SameAs(originalDevelopment));
	}

	[Test]
	public void コンストラクタでゼロ値を受け入れる()
	{
		// Arrange & Act
		var development = new Development(0);

		// Assert
		Assert.That(development.Value, Is.EqualTo(0));
	}
}
