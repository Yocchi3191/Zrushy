using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Entity;

namespace Zrushy.Core.Test.Application.UseCase;

public class InteractPartResultTest
{
	[Test]
	public void コンストラクタでリアクションとイベントを設定できる()
	{
		// Arrange
		var reaction = new Reaction("あっ…", "anim", "expr", "voice");
		var evt = new Event("event_id", "Event Name", EventType.FirstTouch);

		// Act
		var result = new InteractPartResult(reaction, evt);

		// Assert
		Assert.That(result.Reaction, Is.EqualTo(reaction));
		Assert.That(result.Event, Is.EqualTo(evt));
	}

	[Test]
	public void コンストラクタでnullのリアクションを受け入れる()
	{
		// Arrange
		var evt = new Event("event_id", "Event Name", EventType.Other);

		// Act
		var result = new InteractPartResult(null, evt);

		// Assert
		Assert.That(result.Reaction, Is.Null);
		Assert.That(result.Event, Is.EqualTo(evt));
	}

	[Test]
	public void コンストラクタでnullのイベントを受け入れる()
	{
		// Arrange
		var reaction = new Reaction("あっ…", "anim", "expr", "voice");

		// Act
		var result = new InteractPartResult(reaction, null);

		// Assert
		Assert.That(result.Reaction, Is.EqualTo(reaction));
		Assert.That(result.Event, Is.Null);
	}

	[Test]
	public void コンストラクタで両方nullを受け入れる()
	{
		// Act
		var result = new InteractPartResult(null, null);

		// Assert
		Assert.That(result.Reaction, Is.Null);
		Assert.That(result.Event, Is.Null);
	}
}
