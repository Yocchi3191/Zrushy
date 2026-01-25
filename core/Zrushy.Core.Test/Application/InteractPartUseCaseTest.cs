using Zrushy.Core.Application.UseCase.InteractPart;

namespace Zrushy.Core.Test.Application;

public class InteractPartUseCaseTest
{
	InteractPart useCase;

	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void Test1()
	{
		InteractPartCommand command = new InteractPartCommand();
		useCase.Execute(command);
	}
}
