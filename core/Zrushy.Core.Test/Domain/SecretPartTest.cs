// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using Zrushy.Core.Domain.Interactions.Entity;
using Zrushy.Core.Domain.Interactions.ValueObject;

namespace Zrushy.Core.Test.Domain;

/// <summary>
/// SecretPart.CalculateArousal のテスト
/// 乾燥/濡れ状態・処女状態・さわり方・開発度による挙動を検証
/// </summary>
public class SecretPartTest
{
    private static readonly PartConfig s_partConfig = new(-2, 0.1f, 0.05f);
    private static readonly SecretPartConfig s_secretConfig = new(40, 12, 60, 1, 0.1f, 2, 0.2f);
    private static readonly PartID s_partID = new PartID("secret");
    private static readonly Interaction s_finger = new Interaction(s_partID, InteractionType.Finger);
    private static readonly Interaction s_penis = new Interaction(s_partID, InteractionType.Penis);

    // --- 乾燥状態 (WET_THRESHOLD未満) ---

    [Test]
    public void 乾燥状態での指さわりは不快になる()
    {
        SecretPart part = new SecretPart(s_partID, new Development(100), s_secretConfig);
        Arousal result = part.CalculateArousal(new Arousal(0), s_finger, new Affection(100));
        Assert.That(result.Value, Is.LessThan(0));
    }

    [Test]
    public void 乾燥状態での不快感はデフォルトPartより大きい()
    {
        SecretPart secretPart = new SecretPart(s_partID, new Development(0), s_secretConfig);
        Part defaultPart = new Part(s_partID, new Development(0), s_partConfig);

        Arousal secretResult = secretPart.CalculateArousal(new Arousal(0), s_finger, new Affection(0));
        Arousal defaultResult = defaultPart.CalculateArousal(new Arousal(0), s_finger, new Affection(0));

        Assert.That(secretResult.Value, Is.LessThan(defaultResult.Value));
    }

    // --- 濡れ状態 (WET_THRESHOLD以上) ---

    [Test]
    public void 濡れ状態では快感が得られる()
    {
        // WET_THRESHOLD=40以上のArousalで濡れ状態
        SecretPart part = new SecretPart(s_partID, new Development(50), s_secretConfig);
        Arousal result = part.CalculateArousal(new Arousal(40), s_finger, new Affection(50));
        Assert.That(result.Value, Is.GreaterThan(40));
    }

    [Test]
    public void 濡れ状態ではペニスのほうが指より快感が大きい()
    {
        SecretPart part = new SecretPart(s_partID, new Development(50), s_secretConfig, virginityIntact: false);
        Arousal fingerResult = part.CalculateArousal(new Arousal(40), s_finger, new Affection(50));
        Arousal penisResult = part.CalculateArousal(new Arousal(40), s_penis, new Affection(50));

        Assert.That(penisResult.Value, Is.GreaterThan(fingerResult.Value));
    }

    [Test]
    public void 濡れ状態でも開発度が低いとゲインは小さい()
    {
        SecretPart lowDev = new SecretPart(s_partID, new Development(10), s_secretConfig);
        SecretPart highDev = new SecretPart(s_partID, new Development(90), s_secretConfig);

        Arousal lowResult = lowDev.CalculateArousal(new Arousal(40), s_finger, new Affection(0));
        Arousal highResult = highDev.CalculateArousal(new Arousal(40), s_finger, new Affection(0));

        Assert.That(highResult.Value, Is.GreaterThan(lowResult.Value));
    }

    // --- 処女状態 ---

    [Test]
    public void 処女状態でのペニス挿入は大幅に減少する()
    {
        SecretPart part = new SecretPart(s_partID, new Development(100), s_secretConfig, virginityIntact: true);
        Arousal result = part.CalculateArousal(new Arousal(50), s_penis, new Affection(100));
        Assert.That(result.Value, Is.LessThan(0));
    }

    [Test]
    public void 処女状態でのペニス挿入は濡れ状態より大幅に小さい()
    {
        SecretPart part = new SecretPart(s_partID, new Development(100), s_secretConfig, virginityIntact: true);
        Arousal virginResult = part.CalculateArousal(new Arousal(50), s_penis, new Affection(100));

        part.Interact(s_penis); // 処女喪失

        Arousal afterResult = part.CalculateArousal(new Arousal(50), s_penis, new Affection(100));

        Assert.That(afterResult.Value, Is.GreaterThan(virginResult.Value));
    }

    [Test]
    public void 処女喪失後は通常のペニスゲインになる()
    {
        SecretPart part = new SecretPart(s_partID, new Development(50), s_secretConfig, virginityIntact: true);
        part.Interact(s_penis); // 処女喪失

        // 濡れ状態でペニスさわり → 通常ゲイン
        Arousal result = part.CalculateArousal(new Arousal(50), s_penis, new Affection(0));
        Assert.That(result.Value, Is.GreaterThan(50));
    }
}
