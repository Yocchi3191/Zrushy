using NSubstitute;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Entity;
using Zrushy.Core.Domain.Events.Repository;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;
using Zrushy.Core.Domain.Scenarios.ValueObject;
using Zrushy.Core.Presentation;

namespace Zrushy.Core.Test.Presentation
{
	/// <summary>
	/// PartController の単体テスト
	/// PartController は InteractPart を呼ぶだけの薄いレイヤーなので、契約テストのみ
	/// </summary>
	public class PartControllerTest
	{
		private Heroin heroin;
		private IEventRepository repo;
		private IEventBus bus;
		private IInteractionHistory history;
		ClimaxEventConfig _climaxEventConfig = new ClimaxEventConfig(new EventID("climax_scenario"), new ScenarioID("climax_scenario"), 100);

		[SetUp]
		public void setup()
		{
			IEventBus heroinBus = Substitute.For<IEventBus>();
			heroin = new Heroin(heroinBus, _climaxEventConfig);

			IPart headPart = Substitute.For<IPart>();
			headPart.ID.Returns(new PartID("head"));
			headPart.CalculateArousal(Arg.Any<Arousal>(), Arg.Any<Interaction>()).Returns(new Arousal(0));
			heroin.AddPart(headPart);

			repo = Substitute.For<IEventRepository>();
			repo.GetEvents(Arg.Any<PartID>()).Returns(new List<IScenarioEvent>());
			bus = Substitute.For<IEventBus>();
			history = Substitute.For<IInteractionHistory>();
		}

		[Test]
		public void SendInput_頭をさわる入力を受け取ったら_InteractPartが正しいコマンドで呼ばれる()
		{
			// Arrange
			InteractPart interactPart = new InteractPart(heroin, repo, bus, history);
			PartController controller = new PartController(interactPart, new ScenarioInputGate());
			PartInput input = new PartInput(new PartID("head"));

			// Act
			controller.SendInput(input);

			// Assert
			repo.Received(1).GetEvents(Arg.Is<PartID>(id => id.Value == "head"));
		}
	}
}
