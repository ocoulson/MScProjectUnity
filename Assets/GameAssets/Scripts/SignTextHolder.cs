using UnityEngine;
using System.Collections;

public class SignTextHolder : MonoBehaviour {

	SignpostManager sMan;
	InstructionManager iMan;
	public string text;
	// Use this for initialization
	private bool instructionActive = false;
	private bool signpostDisplayed = false;
	private bool signPostActive = false;
	void Start ()
	{
		sMan = GameObject.FindObjectOfType<SignpostManager> ();
		iMan = GameObject.FindObjectOfType<InstructionManager>();
	}

	void ShowReadInstruction ()
	{
		iMan.ShowInstruction ("Press", "Space", "to read");
	}
	void ShowCloseInstruction ()
	{
		iMan.ShowInstruction ("Press", "Space", "to close");
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (!instructionActive && col.gameObject.name == "Player") {
			ShowReadInstruction();
			instructionActive = true;
			signPostActive = true;
		}
	}

	void Update ()
	{
		if (signPostActive && !signpostDisplayed && Input.GetKeyUp (KeyCode.Space)) {
			ShowCloseInstruction ();
			instructionActive = true;
			sMan.ShowSign (text);
			signpostDisplayed = true;
		} else if (signPostActive && signpostDisplayed && Input.GetKeyUp (KeyCode.Space)) {
			ShowReadInstruction ();
			instructionActive = true;
			sMan.HideSign ();
			signpostDisplayed = false;
		}
	}


	void OnTriggerExit2D (Collider2D col)
	{
		if (instructionActive && col.gameObject.name == "Player") {
			iMan.HideInstruction ();
			instructionActive = false;
			if (signpostDisplayed) {
				sMan.HideSign();
				signpostDisplayed = false;
			}
			signPostActive = false;
		}

	}
}
