using NSubstitute;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Entity;
using Zrushy.Core.Domain.Repository;
using Zrushy.Core.Domain.ValueObject;
using Zrushy.Core.Presentation;
using Action = Zrushy.Core.Domain.Entity.Action;

namespace Zrushy.Core.Test.Presentation
{
	/// <summary>
	/// Domain ~ Presentationまでのオブジェクト連携を確認する結合テスト
	/// </summary>
	public class PartControllerTest
	{
		private PartController controller;
		private PartInput input;
		private IScenarioEngine engine;
		private HeroinViewModel heroinViewModel;
		private ScenarioID testScenarioID;

		[SetUp]
		public void Setup()
		{
			testScenarioID = new ScenarioID("test_scenario");

			// Body構築
			Body body = new Body();
			body.AddPart(new Part(new PartID("head"), new Pleasure(0), new Development(0), new Affection(0)));

			// EventRepositoryモック: 発火可能なイベントを返す
			var evt = Substitute.For<IEvent>();
			evt.CanFire().Returns(true);
			evt.Priority.Returns(0);
			evt.ScenarioToStart.Returns(testScenarioID);

			var eventRepository = Substitute.For<IEventRepository>();
			eventRepository.GetEvents(Arg.Any<PartID>()).Returns(new[] { evt });

			// ScenarioEngineモック
			engine = Substitute.For<IScenarioEngine>();
			engine.GetCurrentAction().Returns(new Action("test", "idle", "normal"));

			// 実オブジェクト
			InteractPart useCase = new InteractPart(body, eventRepository);
			heroinViewModel = new HeroinViewModel();
			ScenarioPlayer scenarioPlayer = new ScenarioPlayer(engine, heroinViewModel);

			controller = new PartController(useCase, scenarioPlayer);
			input = new PartInput(new PartID("head"));
		}

		[Test]
		public void SendInputでシナリオが開始される()
		{
			controller.SendInput(input);

			engine.Received(1).Start(testScenarioID);
		}

		[Test]
		public void SendInputでHeroinViewModelが更新される()
		{
			controller.SendInput(input);

			Assert.That(heroinViewModel.CurrentAction, Is.Not.Null);
		}
	}
}
