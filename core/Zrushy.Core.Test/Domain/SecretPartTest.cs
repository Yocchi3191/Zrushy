using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

/// <summary>
/// SecretPart.CalculateArousal のテスト
/// 乾燥/濡れ状態・処女状態・さわり方・開発度による挙動を検証
/// </summary>
public class SecretPartTest
{
	private static readonly PartConfig _partConfig = new(-2, 0.1f, 0.05f);
	private static readonly SecretPartConfig _secretConfig = new(40, 12, 60, 1, 0.1f, 2, 0.2f);
	private static readonly PartID _partID = new PartID("secret");
	private static readonly Interaction _finger = new Interaction(_partID, InteractionType.Finger);
	private static readonly Interaction _penis = new Interaction(_partID, InteractionType.Penis);

	// --- 乾燥状態 (WET_THRESHOLD未満) ---

	[Test]
	public void 乾燥状態での指さわりは不快になる()
	{
		var part = new SecretPart(_partID, new Development(100), new Affection(100), _secretConfig);
		var result = part.CalculateArousal(new Arousal(0), _finger);
		Assert.That(result.Value, Is.LessThan(0));
	}

	[Test]
	public void 乾燥状態での不快感はデフォルトPartより大きい()
	{
		var secretPart = new SecretPart(_partID, new Development(0), new Affection(0), _secretConfig);
		var defaultPart = new Part(_partID, new Development(0), new Affection(0), _partConfig);

		var secretResult = secretPart.CalculateArousal(new Arousal(0), _finger);
		var defaultResult = defaultPart.CalculateArousal(new Arousal(0), _finger);

		Assert.That(secretResult.Value, Is.LessThan(defaultResult.Value));
	}

	// --- 濡れ状態 (WET_THRESHOLD以上) ---

	[Test]
	public void 濡れ状態では快感が得られる()
	{
		// WET_THRESHOLD=40以上のArousalで濡れ状態
		var part = new SecretPart(_partID, new Development(50), new Affection(50), _secretConfig);
		var result = part.CalculateArousal(new Arousal(40), _finger);
		Assert.That(result.Value, Is.GreaterThan(40));
	}

	[Test]
	public void 濡れ状態ではペニスのほうが指より快感が大きい()
	{
		var part = new SecretPart(_partID, new Development(50), new Affection(50), _secretConfig, virginityIntact: false);
		var fingerResult = part.CalculateArousal(new Arousal(40), _finger);
		var penisResult = part.CalculateArousal(new Arousal(40), _penis);

		Assert.That(penisResult.Value, Is.GreaterThan(fingerResult.Value));
	}

	[Test]
	public void 濡れ状態でも開発度が低いとゲインは小さい()
	{
		var lowDev = new SecretPart(_partID, new Development(10), new Affection(0), _secretConfig);
		var highDev = new SecretPart(_partID, new Development(90), new Affection(0), _secretConfig);

		var lowResult = lowDev.CalculateArousal(new Arousal(40), _finger);
		var highResult = highDev.CalculateArousal(new Arousal(40), _finger);

		Assert.That(highResult.Value, Is.GreaterThan(lowResult.Value));
	}

	// --- 処女状態 ---

	[Test]
	public void 処女状態でのペニス挿入は大幅に減少する()
	{
		var part = new SecretPart(_partID, new Development(100), new Affection(100), _secretConfig, virginityIntact: true);
		var result = part.CalculateArousal(new Arousal(50), _penis);
		Assert.That(result.Value, Is.LessThan(0));
	}

	[Test]
	public void 処女状態でのペニス挿入は濡れ状態より大幅に小さい()
	{
		var part = new SecretPart(_partID, new Development(100), new Affection(100), _secretConfig, virginityIntact: true);
		var virginResult = part.CalculateArousal(new Arousal(50), _penis);

		part.Interact(_penis); // 処女喪失

		var afterResult = part.CalculateArousal(new Arousal(50), _penis);

		Assert.That(afterResult.Value, Is.GreaterThan(virginResult.Value));
	}

	[Test]
	public void 処女喪失後は通常のペニスゲインになる()
	{
		var part = new SecretPart(_partID, new Development(50), new Affection(0), _secretConfig, virginityIntact: true);
		part.Interact(_penis); // 処女喪失

		// 濡れ状態でペニスさわり → 通常ゲイン
		var result = part.CalculateArousal(new Arousal(50), _penis);
		Assert.That(result.Value, Is.GreaterThan(50));
	}
}
