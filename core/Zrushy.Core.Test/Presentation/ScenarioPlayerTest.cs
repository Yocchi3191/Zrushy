using NSubstitute;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;
using Zrushy.Core.Presentation;
using Action = Zrushy.Core.Domain.Scenarios.Entity.Action;

namespace Zrushy.Core.Test.Presentation;

public class ScenarioPlayerTest
{
	ScenarioID testscenarioID = new ScenarioID("test_01");
	private Scenario testScenario;


	IScenarioRepository repository;
	HeroinViewModel heroinViewModel;

	ScenarioPlayer player;

	[SetUp]
	public void Setup()
	{
		testScenario = new Scenario(testscenarioID,
			new List<Action> {
				new Action("テストだよ", "test_anim", "test_happy")
			}
		);

		repository = Substitute.For<IScenarioRepository>();
		repository.GetScenario(testscenarioID).Returns(testScenario);

		heroinViewModel = new HeroinViewModel();

		player = new ScenarioPlayer(repository, heroinViewModel);
	}

	[Test]
	public void PlayするとHeroinViewModelにScenarioの最初のActionを送る()
	{
		player.Play(testscenarioID);

		Assert.That(testScenario.First().Equals(heroinViewModel.CurrentAction));
	}

	[Test]
	public void PlayしないとNextしても何も起きない()
	{
		player.Next();
		Assert.That(!testScenario.First().Equals(heroinViewModel.CurrentAction));
	}
}
