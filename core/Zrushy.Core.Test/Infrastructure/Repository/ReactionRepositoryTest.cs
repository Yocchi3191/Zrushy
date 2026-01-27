using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Exception;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;
using Zrushy.Core.Infrastructure.Repository;

namespace Zrushy.Core.Test.Infrastructure.Repository;

public class ReactionRepositoryTest
{
	private IReactionRepository _repository;

	[SetUp]
	public void Setup()
	{
		_repository = new ReactionRepository();
	}

	[Test]
	public void GetReactionでリアクションを取得できる()
	{
		// Arrange
		var partID = new PartID("torso");
		var pleasure = new Pleasure(10);
		var development = new Development(20);
		var affection = new Affection(30);

		// Act
		var reaction = _repository.GetReaction(partID, pleasure, development, affection);

		// Assert
		Assert.That(reaction, Is.Not.Null);
	}

	[Test]
	public void GetReactionでセリフを含むリアクションを取得できる()
	{
		// Arrange
		var partID = new PartID("head");
		var pleasure = new Pleasure(0);
		var development = new Development(0);
		var affection = new Affection(0);

		// Act
		var reaction = _repository.GetReaction(partID, pleasure, development, affection);

		// Assert
		Assert.That(reaction.Dialogue, Is.Not.Null);
		Assert.That(reaction.Dialogue, Is.Not.Empty);
	}

	[Test]
	public void GetReactionでアニメーション名を含むリアクションを取得できる()
	{
		// Arrange
		var partID = new PartID("leg");
		var pleasure = new Pleasure(5);
		var development = new Development(10);
		var affection = new Affection(15);

		// Act
		var reaction = _repository.GetReaction(partID, pleasure, development, affection);

		// Assert
		Assert.That(reaction.AnimationName, Is.Not.Null);
		Assert.That(reaction.AnimationName, Is.Not.Empty);
	}

	[Test]
	public void GetReactionで表情名を含むリアクションを取得できる()
	{
		// Arrange
		var partID = new PartID("arm");
		var pleasure = new Pleasure(0);
		var development = new Development(0);
		var affection = new Affection(0);

		// Act
		var reaction = _repository.GetReaction(partID, pleasure, development, affection);

		// Assert
		Assert.That(reaction.ExpressionName, Is.Not.Null);
		Assert.That(reaction.ExpressionName, Is.Not.Empty);
	}

	[Test]
	public void GetReactionでボイスクリップ名を含むリアクションを取得できる()
	{
		// Arrange
		var partID = new PartID("waist");
		var pleasure = new Pleasure(3);
		var development = new Development(7);
		var affection = new Affection(12);

		// Act
		var reaction = _repository.GetReaction(partID, pleasure, development, affection);

		// Assert
		Assert.That(reaction.VoiceClipName, Is.Not.Null);
		Assert.That(reaction.VoiceClipName, Is.Not.Empty);
	}

	[Test]
	public void GetReactionで異なるパラメータでもリアクションを取得できる()
	{
		// Arrange
		var partID1 = new PartID("torso");
		var partID2 = new PartID("leg");
		var pleasure1 = new Pleasure(10);
		var pleasure2 = new Pleasure(50);

		// Act
		var reaction1 = _repository.GetReaction(partID1, pleasure1, new Development(0), new Affection(0));
		var reaction2 = _repository.GetReaction(partID2, pleasure2, new Development(100), new Affection(100));

		// Assert
		Assert.That(reaction1, Is.Not.Null);
		Assert.That(reaction2, Is.Not.Null);
	}

	[Test]
	public void GetReactionで未定義の部位はUndefinedReactionExceptionを投げる()
	{
		// Arrange
		var partID = new PartID("undefined_part");
		var pleasure = new Pleasure(0);
		var development = new Development(0);
		var affection = new Affection(0);

		// Act & Assert
		Assert.Throws<UndefinedReactionException>(
			() => _repository.GetReaction(partID, pleasure, development, affection));
	}

	[Test]
	public void GetReactionで部位ごとに異なるセリフを取得できる()
	{
		// Arrange
		var pleasure = new Pleasure(0);
		var development = new Development(0);
		var affection = new Affection(0);

		string[] partIds = { "head", "torso", "arm", "hand", "waist", "leg", "foot" };

		// Act
		var dialogues = partIds
			.Select(id => _repository.GetReaction(new PartID(id), pleasure, development, affection).Dialogue)
			.ToList();

		// Assert - 全て非空で、全て異なる
		Assert.That(dialogues, Has.All.Not.Empty);
		Assert.That(dialogues.Distinct().Count(), Is.EqualTo(dialogues.Count),
			"各部位のセリフは全て異なるべき");
	}
}
