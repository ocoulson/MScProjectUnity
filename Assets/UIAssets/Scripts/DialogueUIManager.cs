using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueUIManager : MonoBehaviour {

	public GameObject dialogueBox;
	public Text mainText;
	public Text leftInstruction;
	public Text rightInstruction;
	public Text midInstruction;

	public void ShowDialogueBox(string newText) {
		UpdateText(newText);	
		dialogueBox.SetActive(true);
	}

	public void UpdateText (string newText)
	{
		mainText.text = newText;
	}
	public void HideDialogueBox ()
	{
		dialogueBox.SetActive(false);
	}

	public bool DialogueActive() {
		return dialogueBox.activeInHierarchy;
	}

	public void ShowLeftInstruction (string instruction)
	{
		leftInstruction.text = instruction;
	}

	public void ShowRightInstruction (string instruction)
	{
		rightInstruction.text = instruction;
	}

	public void ShowMidInstruction (string instruction)
	{
		midInstruction.text = instruction;
	}
	public void HideAllInstructions() {
		leftInstruction.text = ""; 
		rightInstruction.text = "";
		midInstruction.text = "";
	}
}
