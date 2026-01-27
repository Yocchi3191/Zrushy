using Zrushy.Core.Domain.ValueObject;

namespace Zrushy.Core.Test.Domain.ValueObject;

public class PartIDTest
{
	[Test]
	public void コンストラクタで文字列値を受け入れる()
	{
		// Arrange
		string value = "head";

		// Act
		var partID = new PartID(value);

		// Assert
		Assert.That(partID, Is.Not.Null);
	}

	[Test]
	public void Equalsで同じ値の場合はtrueを返す()
	{
		// Arrange
		var partID1 = new PartID("chest");
		var partID2 = new PartID("chest");

		// Act & Assert
		Assert.That(partID1.Equals(partID2), Is.True);
	}

	[Test]
	public void Equalsで異なる値の場合はfalseを返す()
	{
		// Arrange
		var partID1 = new PartID("head");
		var partID2 = new PartID("chest");

		// Act & Assert
		Assert.That(partID1.Equals(partID2), Is.False);
	}

	[Test]
	public void Equalsでnullの場合はfalseを返す()
	{
		// Arrange
		var partID = new PartID("head");

		// Act & Assert
		Assert.That(partID.Equals(null), Is.False);
	}

	[Test]
	public void Equalsで異なる型の場合はfalseを返す()
	{
		// Arrange
		var partID = new PartID("head");
		var notPartID = "head";

		// Act & Assert
		Assert.That(partID.Equals(notPartID), Is.False);
	}

	[Test]
	public void GetHashCodeで等しいオブジェクトは同じ値を返す()
	{
		// Arrange
		var partID1 = new PartID("leg");
		var partID2 = new PartID("leg");

		// Act
		int hashCode1 = partID1.GetHashCode();
		int hashCode2 = partID2.GetHashCode();

		// Assert
		Assert.That(hashCode1, Is.EqualTo(hashCode2));
	}

	[Test]
	public void GetHashCodeで異なるオブジェクトは異なる値を返す()
	{
		// Arrange
		var partID1 = new PartID("arm");
		var partID2 = new PartID("leg");

		// Act
		int hashCode1 = partID1.GetHashCode();
		int hashCode2 = partID2.GetHashCode();

		// Assert
		Assert.That(hashCode1, Is.Not.EqualTo(hashCode2));
	}

	[Test]
	public void コンストラクタで空文字列を受け入れる()
	{
		// Arrange & Act
		var partID = new PartID("");

		// Assert
		Assert.That(partID, Is.Not.Null);
	}
}
