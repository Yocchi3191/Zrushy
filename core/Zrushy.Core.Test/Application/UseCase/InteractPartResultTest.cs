using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Scenarios.ValueObject;

namespace Zrushy.Core.Test.Application.UseCase;

public class InteractPartResultTest
{
	[Test]
	public void ScenarioToStartにScenarioIDを設定できる()
	{
		var scenarioID = new ScenarioID("test");
		var result = new InteractPartResult(scenarioID);

		Assert.That(result.ScenarioToStart, Is.EqualTo(scenarioID));
	}
}
