using UnityEngine;
using System.Collections;

public class SignTextHolder : MonoBehaviour {

	SignpostManager sMan;
	InstructionManager iMan;
	public string signText;

	public bool hasDirections;
	public string[] directionArrows;
	public string[] directionText;
	// Use this for initialization

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
		if (!iMan.IsActive() && col.gameObject.name == "Player") {
			ShowReadInstruction();

			signPostActive = true;
		}
	}

	void Update ()
	{
		if (signPostActive && !signpostDisplayed && Input.GetKeyUp (KeyCode.Space)) {
			ShowCloseInstruction ();
			if (hasDirections) {
				sMan.ShowSignDirections (directionArrows, directionText);
			} else {
				Debug.Log(signText);
				sMan.ShowSignText(signText);

			}

			signpostDisplayed = true;
		} else if (signPostActive && signpostDisplayed && Input.GetKeyUp (KeyCode.Space)) {
			ShowReadInstruction ();
			sMan.HideSign ();
			signpostDisplayed = false;
		}
	}


	void OnTriggerExit2D (Collider2D col)
	{
		if (iMan.IsActive() && col.gameObject.name == "Player") {
			iMan.HideInstruction ();
			if (signpostDisplayed) {
				sMan.HideSign();
				signpostDisplayed = false;
			}
			signPostActive = false;
		}

	}


}
