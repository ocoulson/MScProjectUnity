using UnityEngine;
using System.Collections;

public class SignTextHolder : MonoBehaviour {

	SignpostManager sMan;
	InstructionManager iMan;
	public string text;
	// Use this for initialization
	private bool instructionActive = false;
	void Start ()
	{
		sMan = GameObject.FindObjectOfType<SignpostManager> ();
		iMan = GameObject.FindObjectOfType<InstructionManager>();
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (!instructionActive && col.gameObject.name == "Player") {
			iMan.DisplayInstruction("Press", "Space", "to read");
			instructionActive = true;
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{	
		//Debug.Log("Sign Triggered");
		if (col.gameObject.name == "Player" && Input.GetKey(KeyCode.Space)) {
			iMan.RemoveInstruction();
			instructionActive = false;
			sMan.ShowSign(text);
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (instructionActive && col.gameObject.name == "Player") {
			iMan.RemoveInstruction();
			instructionActive = false;
		}

	}
}
