using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

/// <summary>
/// 絶頂フローのテスト
/// Heroin と Part の結合による Arousal 蓄積を検証する
/// Arousal 値オブジェクト自体の絶頂判定・クールダウン挙動は ArousalTest で検証
/// </summary>
public class ClimaxFlowTest
{
	private const int CLIMAX_THRESHOLD = 100;
	private static readonly PartConfig _partConfig = new(-2, 0.1f, 0.05f);
	private Heroin _body;
	private PartID _partID;

	[SetUp]
	public void Setup()
	{
		_body = new Heroin();
		_partID = new PartID("test");

		// 開発度50、好感度50の部位を追加
		_body.AddPart(new Part(_partID, new Development(50), new Affection(50), _partConfig));
	}

	[Test]
	public void 快感が閾値を超えると絶頂状態になる()
	{
		// Act: Interact を繰り返して快感を蓄積
		// 開発度50、好感度50なので、1回あたり約5増加 (-2 + 5 + 2.5)
		// 100 / 5 ≒ 20回で絶頂
		for (int i = 0; i < 25; i++)
		{
			_body.Interact(new Interaction(_partID));
		}

		Assert.That(_body.Arousal.IsAboveThreshold(CLIMAX_THRESHOLD), Is.True);
	}
}
