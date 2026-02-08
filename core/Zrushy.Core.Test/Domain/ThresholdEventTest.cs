using Zrushy.Core.Domain.Events.Entity.Conditions;
using Zrushy.Core.Domain.Events.ValueObject;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

public class ThresholdEventTest
{
	private PartID _partID;

	[SetUp]
	public void Setup()
	{
		_partID = new PartID("head");
	}

	[Test]
	public void 快感が閾値範囲内なら発火する()
	{
		var reader = new StubPartParameterReader(pleasure: new Pleasure(50));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(new Pleasure(10), new Pleasure(100), () => reader.GetPleasure(_partID)));

		Assert.That(condition.CanFire(), Is.True);
	}

	[Test]
	public void 快感が閾値範囲外なら発火しない()
	{
		var reader = new StubPartParameterReader(pleasure: new Pleasure(5));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(new Pleasure(10), new Pleasure(100), () => reader.GetPleasure(_partID)));

		Assert.That(condition.CanFire(), Is.False);
	}

	[Test]
	public void 指定していないパラメータは無視される()
	{
		var reader = new StubPartParameterReader(
			pleasure: new Pleasure(50),
			development: new Development(999),
			affection: new Affection(999));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(new Pleasure(10), new Pleasure(100), () => reader.GetPleasure(_partID)));

		Assert.That(condition.CanFire(), Is.True);
	}

	[Test]
	public void 閾値なしなら常に発火する()
	{
		var condition = new ThresholdCondition();

		Assert.That(condition.CanFire(), Is.True);
	}

	[Test]
	public void Minだけ指定で下限のみ判定できる()
	{
		var reader = new StubPartParameterReader(pleasure: new Pleasure(50));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(new Pleasure(10), null, () => reader.GetPleasure(_partID)));

		Assert.That(condition.CanFire(), Is.True);
	}

	[Test]
	public void Minだけ指定で下限未満なら発火しない()
	{
		var reader = new StubPartParameterReader(pleasure: new Pleasure(5));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(new Pleasure(10), null, () => reader.GetPleasure(_partID)));

		Assert.That(condition.CanFire(), Is.False);
	}

	[Test]
	public void Maxだけ指定で上限のみ判定できる()
	{
		var reader = new StubPartParameterReader(pleasure: new Pleasure(50));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(null, new Pleasure(100), () => reader.GetPleasure(_partID)));

		Assert.That(condition.CanFire(), Is.True);
	}

	[Test]
	public void Maxだけ指定で上限超過なら発火しない()
	{
		var reader = new StubPartParameterReader(pleasure: new Pleasure(150));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(null, new Pleasure(100), () => reader.GetPleasure(_partID)));

		Assert.That(condition.CanFire(), Is.False);
	}

	[Test]
	public void 複数パラメータの閾値を同時に判定できる()
	{
		var reader = new StubPartParameterReader(
			pleasure: new Pleasure(50),
			development: new Development(30));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(new Pleasure(10), new Pleasure(100), () => reader.GetPleasure(_partID)),
			new Threshold<Development>(new Development(10), new Development(50), () => reader.GetDevelopment(_partID)));

		Assert.That(condition.CanFire(), Is.True);
	}

	[Test]
	public void 複数パラメータのうち1つでも範囲外なら発火しない()
	{
		var reader = new StubPartParameterReader(
			pleasure: new Pleasure(50),
			development: new Development(60));

		var condition = new ThresholdCondition(
			new Threshold<Pleasure>(new Pleasure(10), new Pleasure(100), () => reader.GetPleasure(_partID)),
			new Threshold<Development>(new Development(10), new Development(50), () => reader.GetDevelopment(_partID)));

		Assert.That(condition.CanFire(), Is.False);
	}

	// --- Stub ---

	private class StubPartParameterReader : IPartParameterReader
	{
		private readonly Pleasure _pleasure;
		private readonly Development _development;
		private readonly Affection _affection;

		public StubPartParameterReader(
			Pleasure? pleasure = null,
			Development? development = null,
			Affection? affection = null)
		{
			_pleasure = pleasure ?? new Pleasure(0);
			_development = development ?? new Development(0);
			_affection = affection ?? new Affection(0);
		}

		public Pleasure GetPleasure(PartID partID) => _pleasure;
		public Development GetDevelopment(PartID partID) => _development;
		public Affection GetAffection(PartID partID) => _affection;
	}
}
