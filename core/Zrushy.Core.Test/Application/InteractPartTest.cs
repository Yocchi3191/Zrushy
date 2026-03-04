using NSubstitute;
using Zrushy.Core.Application.UseCase.InteractPart;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.Exception;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Application;

public class InteractPartTest
{
	private static readonly PartConfig _partConfig = new(-2, 0.1f, 0.05f);
	private PartID _partID;
	private Heroin _body;

	[SetUp]
	public void Setup()
	{
		_partID = new PartID("head");
		_body = new Heroin(Substitute.For<IEventEvaluator>());
		_body.AddPart(new Part(_partID, new Development(0), new Affection(0), _partConfig));
	}

	[Test]
	public void Executeでパラメータが更新される()
	{
		var useCase = new InteractPart(_body);

		useCase.Execute(new InteractPartCommand(_partID));

		var part = _body.GetPart(_partID);
		// 好感度0・開発度0の部位は不快なので興奮度は負になる
		Assert.That(_body.Arousal.Value, Is.LessThan(0));
		Assert.That(part.Development.Value, Is.EqualTo(1));
		Assert.That(part.Affection.Value, Is.EqualTo(1));
	}

	[Test]
	public void Executeで存在しないパーツは例外を投げる()
	{
		var useCase = new InteractPart(_body);

		Assert.Throws<PartNotFoundException>(() =>
			useCase.Execute(new InteractPartCommand(new PartID("nonexistent"))));
	}
}
