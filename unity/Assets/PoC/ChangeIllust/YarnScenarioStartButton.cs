// Copyright (c) yoshioyocchi314@gmail.com
// Licensed under the MIT License.

using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Zrushy.Core.Presentation.Debug
{
	public class YarnScenarioStartButton : MonoBehaviour
	{
		[SerializeField] private DialogueRunner _dialogueRunner;
		[SerializeField] private string _startNode = "Start";

		private void Start()
		{
			GetComponent<Button>().onClick.AddListener(OnClick);
		}

		private void OnClick()
		{
			if (_dialogueRunner.IsDialogueRunning)
				return;

			_dialogueRunner.StartDialogue(_startNode);
		}
	}
}