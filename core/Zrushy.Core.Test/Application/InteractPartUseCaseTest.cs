using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;
using Zrushy.Core.Infrastructure.Repository;

namespace Zrushy.Core.Test.Application;

public class InteractPartUseCaseTest
{
	// Test Data
	PartID partID = new PartID("part");

	// Domain
	Body body = new Body();
	IReactionRepository reactionRepository;
	IEventRepository eventRepository;

	// Application
	InteractPart useCase;

	[SetUp]
	public void Setup()
	{
		reactionRepository = new ReactionRepository();
		eventRepository = new EventRepository();
		useCase = new InteractPart(body, reactionRepository, eventRepository);
	}

	[Test]
	public void Test1()
	{
		InteractPartCommand command = new InteractPartCommand(partID);
		InteractPartResult result = useCase.Execute(command);

		// リアクションが返されることを確認（ダミーデータ）
		Assert.That(result.Reaction, Is.Null); // Bodyが空なので部位が見つからずnull
	}
}
