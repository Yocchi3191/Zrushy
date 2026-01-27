using Zrushy.Core.Domain.Entity;

namespace Zrushy.Core.Test.Domain.Entity;

public class EventTest
{
	[Test]
	public void コンストラクタで全てのプロパティを設定できる()
	{
		// Arrange
		string eventID = "first_touch_head";
		string name = "初めて頭をさわられる";
		EventType type = EventType.FirstTouch;
		string cutsceneName = "cutscene_first_head_touch";

		// Act
		var evt = new Event(eventID, name, type, cutsceneName);

		// Assert
		Assert.That(evt.EventID, Is.EqualTo(eventID));
		Assert.That(evt.Name, Is.EqualTo(name));
		Assert.That(evt.Type, Is.EqualTo(type));
		Assert.That(evt.CutsceneName, Is.EqualTo(cutsceneName));
	}

	[Test]
	public void コンストラクタでカットシーン名のデフォルト値を設定する()
	{
		// Arrange
		string eventID = "event_01";
		string name = "テストイベント";
		EventType type = EventType.Other;

		// Act
		var evt = new Event(eventID, name, type);

		// Assert
		Assert.That(evt.CutsceneName, Is.EqualTo(""));
	}

	[Test]
	public void コンストラクタで全てのイベントタイプを受け入れる()
	{
		// Arrange & Act
		var firstTouch = new Event("e1", "First", EventType.FirstTouch);
		var developmentThreshold = new Event("e2", "Dev", EventType.DevelopmentThreshold);
		var climax = new Event("e3", "Climax", EventType.Climax);
		var conditioningComplete = new Event("e4", "Cond", EventType.ConditioningComplete);
		var other = new Event("e5", "Other", EventType.Other);

		// Assert
		Assert.That(firstTouch.Type, Is.EqualTo(EventType.FirstTouch));
		Assert.That(developmentThreshold.Type, Is.EqualTo(EventType.DevelopmentThreshold));
		Assert.That(climax.Type, Is.EqualTo(EventType.Climax));
		Assert.That(conditioningComplete.Type, Is.EqualTo(EventType.ConditioningComplete));
		Assert.That(other.Type, Is.EqualTo(EventType.Other));
	}

	[Test]
	public void プロパティは読み取り専用である()
	{
		// Arrange
		var evt = new Event("id", "name", EventType.Other, "cutscene");

		// Act & Assert
		Assert.That(evt.EventID, Is.Not.Null);
		Assert.That(evt.Name, Is.Not.Null);
		Assert.That(evt.CutsceneName, Is.Not.Null);
	}
}
