using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Zrushy.Core.Presentation;

public class ScenarioDriver : MonoBehaviour
{
	[Inject]
	private ScenarioPlayer scenarioPlayer;

	private void Update()
	{
		if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
		{
			scenarioPlayer.Next();
		}
	}
}
