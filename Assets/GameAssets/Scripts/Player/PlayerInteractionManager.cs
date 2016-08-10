using UnityEngine;
using System.Collections;

public class PlayerInteractionManager : MonoBehaviour {

	private ThoughtBubbleManager bubbleManager;
	private DialogueBlock[] thoughts;

	// Use this for initialization
	void Start () {
		bubbleManager = GameObject.FindObjectOfType<ThoughtBubbleManager>();
	}
	
	public void DisplayThoughtBubble (int thoughtDialogueId)
	{

		if (thoughts == null) {
			ReadJSON reader = FindObjectOfType<ReadJSON> ();
			thoughts = reader.GetCharacterDialogue ("playerThoughtBubbles");
		}

		string text = "";

		foreach (DialogueBlock dBlock in thoughts) {
			if (dBlock.id == thoughtDialogueId) {
				text = dBlock.script_en_GB[0];
			}
		}
		bubbleManager.ShowThoughtBubble(text);
	}

	public void HideThoughtBubble() {
		Debug.Log("Hide Thought Bubble called");
		bubbleManager.HideThoughtBubble();
	}

	public bool IsThoughtBubbleActive ()
	{
		return bubbleManager.IsThoughtBubbleActive();
	}
}
