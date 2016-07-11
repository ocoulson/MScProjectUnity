using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ThoughtBubbleManager : MonoBehaviour {
	public GameObject thoughtBubble;
	public Text bubbleText;

	void Start() {
		thoughtBubble.SetActive(false);
	}

	public void ShowThoughtBubble (string text)
	{
		thoughtBubble.SetActive(true);
		bubbleText.text = text;
	}

	public void HideThoughtBubble ()
	{
		thoughtBubble.SetActive(false);
	}

	public bool IsThoughtBubbleActive() {
		return thoughtBubble.activeInHierarchy;
	}
}
