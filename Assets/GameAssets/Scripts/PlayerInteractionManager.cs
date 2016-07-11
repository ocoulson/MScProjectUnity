using UnityEngine;
using System.Collections;

public class PlayerInteractionManager : MonoBehaviour {

	private ThoughtBubbleManager bubbleManager;
	// Use this for initialization
	void Start () {
		bubbleManager = GameObject.FindObjectOfType<ThoughtBubbleManager>();
	}
	
	public void DisplayThoughtBubble(string text) {
		Debug.Log("Display Thought Bubble called");
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
