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

	private Action action1 = new Action("test_1", "test_anim", "test_happy");
	private Action action2 = new Action("test_2", "test_anim", "test_happy");

	IScenarioRepository repository;
	HeroinViewModel heroinViewModel;

	ScenarioPlayer player;

	[SetUp]
	public void Setup()
	{
		testScenario = new Scenario(testscenarioID,
			new List<Action> { action1, action2 }
		);

		repository = Substitute.For<IScenarioRepository>();
		repository.When(x => x.Start(Arg.Any<ScenarioID>()))
			.Do(_ => repository.OnActionChanged += Raise.Event<System.Action<Action>>(action1));
		repository.When(x => x.Next())
			.Do(_ => repository.OnActionChanged += Raise.Event<System.Action<Action>>(action2));

		heroinViewModel = new HeroinViewModel();

		player = new ScenarioPlayer(repository, heroinViewModel);
	}

	[Test]
	public void PlayするとHeroinViewModelにScenarioの最初のActionを送る()
	{
		player.Play(testscenarioID);

		Assert.That(heroinViewModel.CurrentAction, Is.EqualTo(action1));
	}

	[Test]
	public void PlayしないとNextしても何も起きない()
	{
		player.Next();
		Assert.That(heroinViewModel.CurrentAction, Is.Not.EqualTo(action1));
	}

	[Test]
	public void PlayしたらNextでScenarioを進められる()
	{
		player.Play(testscenarioID);

		Assert.That(heroinViewModel.CurrentAction, Is.EqualTo(action1));
		player.Next();
		Assert.That(heroinViewModel.CurrentAction, Is.EqualTo(action2));
	}
}
