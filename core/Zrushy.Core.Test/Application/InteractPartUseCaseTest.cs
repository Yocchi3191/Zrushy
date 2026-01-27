using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Exception;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;
using Zrushy.Core.Infrastructure.Repository;

namespace Zrushy.Core.Test.Application;

public class InteractPartUseCaseTest
{
	private PartID _partID;
	private Body _body;
	private IReactionRepository _reactionRepository;
	private IEventRepository _eventRepository;
	private InteractPart _useCase;

	[SetUp]
	public void Setup()
	{
		_partID = new PartID("head");
		_body = new Body();
		_reactionRepository = new ReactionRepository();
		_eventRepository = new EventRepository();
		_useCase = new InteractPart(_body, _reactionRepository, _eventRepository);
	}

	[Test]
	public void Executeで存在しないパーツの場合はPartNotFoundExceptionを投げる()
	{
		// Arrange
		var command = new InteractPartCommand(new PartID("nonexistent"));

		// Act & Assert
		Assert.Throws<PartNotFoundException>(() => _useCase.Execute(command));
	}

	[Test]
	public void Executeで存在するパーツの場合はリアクションを返す()
	{
		// Arrange
		var part = new Part(_partID, new Pleasure(0), new Development(0), new Affection(0));
		_body.AddPart(part);
		var command = new InteractPartCommand(_partID);

		// Act
		var result = _useCase.Execute(command);

		// Assert
		Assert.That(result.Reaction, Is.Not.Null);
	}

	[Test]
	public void Executeで存在するパーツの場合はパラメータを更新する()
	{
		// Arrange
		var part = new Part(_partID, new Pleasure(0), new Development(0), new Affection(0));
		_body.AddPart(part);
		var command = new InteractPartCommand(_partID);

		// Act
		_useCase.Execute(command);

		// Assert
		var updatedPart = _body.GetPart(_partID);
		Assert.That(updatedPart!.Pleasure.Value, Is.EqualTo(1));
		Assert.That(updatedPart.Development.Value, Is.EqualTo(1));
		Assert.That(updatedPart.Affection.Value, Is.EqualTo(1));
	}

	[Test]
	public void Executeを複数回実行するとパラメータが累積する()
	{
		// Arrange
		var part = new Part(_partID, new Pleasure(0), new Development(0), new Affection(0));
		_body.AddPart(part);
		var command = new InteractPartCommand(_partID);

		// Act
		_useCase.Execute(command);
		_useCase.Execute(command);
		_useCase.Execute(command);

		// Assert
		var updatedPart = _body.GetPart(_partID);
		Assert.That(updatedPart!.Pleasure.Value, Is.EqualTo(3));
		Assert.That(updatedPart.Development.Value, Is.EqualTo(3));
		Assert.That(updatedPart.Affection.Value, Is.EqualTo(3));
	}

	[Test]
	public void Executeでリポジトリからリアクションを取得する()
	{
		// Arrange
		var part = new Part(_partID, new Pleasure(5), new Development(10), new Affection(15));
		_body.AddPart(part);
		var command = new InteractPartCommand(_partID);

		// Act
		var result = _useCase.Execute(command);

		// Assert
		Assert.That(result.Reaction, Is.Not.Null);
		Assert.That(result.Reaction!.Dialogue, Is.EqualTo("や、やめて…髪が…"));
		Assert.That(result.Reaction.AnimationName, Is.EqualTo("reaction_default"));
		Assert.That(result.Reaction.ExpressionName, Is.EqualTo("expression_shy"));
		Assert.That(result.Reaction.VoiceClipName, Is.EqualTo("voice_reaction_01"));
	}

	[Test]
	public void Executeで現在の実装ではイベントはnullを返す()
	{
		// Arrange
		var part = new Part(_partID, new Pleasure(0), new Development(0), new Affection(0));
		_body.AddPart(part);
		var command = new InteractPartCommand(_partID);

		// Act
		var result = _useCase.Execute(command);

		// Assert
		Assert.That(result.Event, Is.Null);
	}

	[Test]
	public void Executeで異なるパーツをそれぞれ独立して処理する()
	{
		// Arrange
		var partID1 = new PartID("head");
		var partID2 = new PartID("torso");
		var part1 = new Part(partID1, new Pleasure(0), new Development(0), new Affection(0));
		var part2 = new Part(partID2, new Pleasure(0), new Development(0), new Affection(0));
		_body.AddPart(part1);
		_body.AddPart(part2);

		var command1 = new InteractPartCommand(partID1);
		var command2 = new InteractPartCommand(partID2);

		// Act
		_useCase.Execute(command1);
		_useCase.Execute(command1);
		_useCase.Execute(command2);

		// Assert
		var updatedPart1 = _body.GetPart(partID1);
		var updatedPart2 = _body.GetPart(partID2);
		Assert.That(updatedPart1!.Pleasure.Value, Is.EqualTo(2));
		Assert.That(updatedPart2!.Pleasure.Value, Is.EqualTo(1));
	}

	[Test]
	public void Executeで登録済みパーツに対して例外を投げない()
	{
		// Arrange
		var part = new Part(_partID, new Pleasure(0), new Development(0), new Affection(0));
		_body.AddPart(part);
		var command = new InteractPartCommand(_partID);

		// Act & Assert
		Assert.DoesNotThrow(() => _useCase.Execute(command));
	}
}
