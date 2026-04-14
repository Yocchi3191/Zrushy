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
    [Inject] private IAffectionReader _affectionReader;
    [Inject] private IArousalReader _arousalReader;
    [Inject] private IDevelopmentReader _developmentReader;

    [Inject]
    private IInteractionHistory _interactionHistory;

    [SerializeField]
    private TextMeshProUGUI _debugText;

    [SerializeField]
    private List<string> _parts = new List<string> { "head", "torso", "hand", "arm", "waist", "foot", "leg" };

    private void Update()
    {
        if (_debugText == null)
            return;

        string text = "=== Part Parameters ===\n\n";

        // 興奮度
        Arousal arousal = _arousalReader.GetArousal();
        text += $"[Arousal: {arousal.Value}]\n";
        // 好感度
        Affection affection = _affectionReader.GetAffection();
        text += $"[Affection: {affection.Value}]\n\n";

        foreach (string partName in _parts)
        {
            PartID partID = new PartID(partName);
            int touchCount = _interactionHistory.Get(partID).Count;
            Development development = _developmentReader.GetDevelopment(partID);

            text += $"[{partName}]\n";
            text += $"  Touch: {touchCount}\n";
            text += $"  Development: {development.Value}\n";
        }

        _debugText.text = text;
    }
}
