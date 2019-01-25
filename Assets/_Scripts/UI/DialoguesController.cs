using System;
using Core;
using Core.Inputs;
using TMPro;
using UnityEngine;

namespace UI {
	public class DialoguesController : MonoBehaviour {

		public CanvasGroup dialogueCanvasGroup;
		public TextMeshProUGUI textComponent;

		public ContextualInputsState inDialogueInputsState;
		public ContextualInputsState afterDialogueInputsState;

		private void Start() {
			dialogueCanvasGroup.gameObject.SetActive(false);
		}


		public void StartDialogue() {
			GameController.Instance.actionsMapsHelper.ApplyContext(inDialogueInputsState);
			dialogueCanvasGroup.gameObject.SetActive(true);
		}


		public void EndDialogue() {
			GameController.Instance.actionsMapsHelper.ApplyContext(afterDialogueInputsState);
			dialogueCanvasGroup.gameObject.SetActive(false);
		}

		public void SetCurrentText(String text) {
			textComponent.SetText(text);
		}

	}
}
