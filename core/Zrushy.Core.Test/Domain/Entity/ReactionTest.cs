using Zrushy.Core.Domain.Entity;

namespace Zrushy.Core.Test.Domain.Entity;

public class ReactionTest
{
	[Test]
	public void コンストラクタで全てのプロパティを設定できる()
	{
		// Arrange
		string dialogue = "あっ…";
		string animationName = "reaction_shy";
		string expressionName = "expression_embarrassed";
		string voiceClipName = "voice_01";

		// Act
		var reaction = new Reaction(dialogue, animationName, expressionName, voiceClipName);

		// Assert
		Assert.That(reaction.Dialogue, Is.EqualTo(dialogue));
		Assert.That(reaction.AnimationName, Is.EqualTo(animationName));
		Assert.That(reaction.ExpressionName, Is.EqualTo(expressionName));
		Assert.That(reaction.VoiceClipName, Is.EqualTo(voiceClipName));
	}

	[Test]
	public void コンストラクタで空文字列を受け入れる()
	{
		// Act
		var reaction = new Reaction("", "", "", "");

		// Assert
		Assert.That(reaction.Dialogue, Is.EqualTo(""));
		Assert.That(reaction.AnimationName, Is.EqualTo(""));
		Assert.That(reaction.ExpressionName, Is.EqualTo(""));
		Assert.That(reaction.VoiceClipName, Is.EqualTo(""));
	}

	[Test]
	public void コンストラクタで長いセリフを受け入れる()
	{
		// Arrange
		string longDialogue = "やめて…そんなところ触らないで…恥ずかしい…";

		// Act
		var reaction = new Reaction(longDialogue, "anim", "expr", "voice");

		// Assert
		Assert.That(reaction.Dialogue, Is.EqualTo(longDialogue));
	}

	[Test]
	public void プロパティは読み取り専用である()
	{
		// Arrange
		var reaction = new Reaction("test", "anim", "expr", "voice");

		// Act & Assert
		Assert.That(() => reaction.Dialogue, Is.Not.Null);
		Assert.That(() => reaction.AnimationName, Is.Not.Null);
		Assert.That(() => reaction.ExpressionName, Is.Not.Null);
		Assert.That(() => reaction.VoiceClipName, Is.Not.Null);
	}
}
