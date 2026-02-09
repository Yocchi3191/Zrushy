using UnityEngine;
using TMPro;
using Zenject;
using Zrushy.Core.Domain.Events.Service;
using Zrushy.Core.Domain.Interactions.Service;
using Zrushy.Core.Domain.Interactions.ValueObject;

public class PartParameterDebugView : MonoBehaviour
{
	[Inject]
	private IPartParameterReader parameterReader;

	[Inject]
	private IInteractionHistory interactionHistory;

	[SerializeField]
	private TextMeshProUGUI debugText;

	private readonly string[] parts = { "head", "hand", "arm", "waist", "foot", "leg" };

	private void Update()
	{
		if (debugText == null) return;

		var text = "=== Part Parameters ===\n\n";

		foreach (var partName in parts)
		{
			var partID = new PartID(partName);
			var touchCount = interactionHistory.Get(partID).Count;
			var pleasure = parameterReader.GetPleasure(partID);
			var development = parameterReader.GetDevelopment(partID);
			var affection = parameterReader.GetAffection(partID);

			text += $"[{partName}]\n";
			text += $"  Touch: {touchCount}\n";
			text += $"  Pleasure: {pleasure}\n";
			text += $"  Development: {development}\n";
			text += $"  Affection: {affection}\n\n";
		}

		debugText.text = text;
	}
}
