// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

public class PartParameterDebugView : MonoBehaviour
{
    [Inject]
    private IPartParameterReader _parameterReader;

    [Inject]
    private IInteractionHistory _interactionHistory;

    [SerializeField]
    private TextMeshProUGUI _debugText;

    [SerializeField]
    private List<string> _parts = new List<string> { "head", "torso", "hand", "arm", "waist", "foot", "leg" };

    private void Update()
    {
        if (_debugText == null) return;

        string text = "=== Part Parameters ===\n\n";

        // Body 全体の快感を表示
        Arousal bodyArousal = _parameterReader.GetArousal(new PartID("dummy")); // PartID は無視される
        text += $"[Body Arousal: {bodyArousal.Value}]\n\n";

        foreach (string partName in _parts)
        {
            PartID partID = new PartID(partName);
            int touchCount = _interactionHistory.Get(partID).Count;
            Development development = _parameterReader.GetDevelopment(partID);
            Affection affection = _parameterReader.GetAffection(partID);

            text += $"[{partName}]\n";
            text += $"  Touch: {touchCount}\n";
            text += $"  Development: {development}\n";
            text += $"  Affection: {affection}\n\n";
        }

        _debugText.text = text;
    }
}
