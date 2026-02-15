using NSubstitute;
using Zrushy.Core.Domain.Scenarios.Entity;
using Zrushy.Core.Domain.Scenarios.Repository;
using Zrushy.Core.Domain.Scenarios.ValueObject;
using Zrushy.Core.Presentation;
using Beat = Zrushy.Core.Domain.Scenarios.Entity.Beat;

namespace Zrushy.Core.Test.Presentation;

public class ScenarioPlayerTest
{
	ScenarioID testscenarioID = new ScenarioID("test_01");
	private Scenario testScenario;

	private Beat beat1 = new Beat("test_1", "test_anim", "test_happy");
	private Beat beat2 = new Beat("test_2", "test_anim", "test_happy");

	IScenarioRepository repository;
	HeroinViewModel heroinViewModel;

	ScenarioPlayer player;

	[SetUp]
	public void Setup()
	{
		testScenario = new Scenario(testscenarioID,
			new List<Beat> { beat1, beat2 }
		);

		repository = Substitute.For<IScenarioRepository>();
		repository.When(x => x.Start(Arg.Any<ScenarioID>()))
			.Do(_ => repository.OnBeatChanged += Raise.Event<System.Action<Beat>>(beat1));
		repository.When(x => x.Next())
			.Do(_ => repository.OnBeatChanged += Raise.Event<System.Action<Beat>>(beat2));

		heroinViewModel = new HeroinViewModel();

		player = new ScenarioPlayer(repository, heroinViewModel);
	}

	[Test]
	public void PlayするとHeroinViewModelにScenarioの最初のBeatを送る()
	{
		player.Play(testscenarioID);

		Assert.That(heroinViewModel.CurrentBeat, Is.EqualTo(beat1));
	}

	[Test]
	public void PlayしないとNextしても何も起きない()
	{
		player.Next();
		Assert.That(heroinViewModel.CurrentBeat, Is.Not.EqualTo(beat1));
	}

	[Test]
	public void PlayしたらNextでScenarioを進められる()
	{
		player.Play(testscenarioID);

		Assert.That(heroinViewModel.CurrentBeat, Is.EqualTo(beat1));
		player.Next();
		Assert.That(heroinViewModel.CurrentBeat, Is.EqualTo(beat2));
	}
}
