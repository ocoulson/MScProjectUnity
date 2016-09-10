using UnityEngine;
using System.Collections;

public class SignTextHolder : MonoBehaviour {
	SpriteRenderer spriteRenderer;
	SignpostManager sMan;
	InstructionManager iMan;
	public string signText;

	public bool hasDirections;
	public string[] directionArrows;
	public string[] directionText;

	private bool signpostDisplayed = false;
	private bool signPostActive = false;


	void Start ()
	{
		sMan = GameObject.FindObjectOfType<SignpostManager> ();
		iMan = GameObject.FindObjectOfType<InstructionManager> ();
		spriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer> ();

	}

	void ShowReadInstruction ()
	{
		iMan.ShowInstruction ("Press", "Space", "to read");
	}
	void ShowCloseInstruction ()
	{
		iMan.ShowInstruction ("Press", "Space", "to close");
	}
	
	void OnTriggerStay2D (Collider2D col)
	{
		ChangeSortingOrder (col);
		if (!iMan.IsActive () && col.gameObject.tag == "Player") {
			ShowReadInstruction ();
		}
	}

	void Update ()
	{
		if (signPostActive && !signpostDisplayed && Input.GetKeyUp (KeyCode.Space)) {
			ShowCloseInstruction ();
			if (hasDirections) {
				sMan.ShowSignDirections (directionArrows, directionText);
			} else {
				sMan.ShowSignText(signText);

			}
			Debug.Log(spriteRenderer.sortingOrder);
			signpostDisplayed = true;
		} else if (signPostActive && signpostDisplayed && Input.GetKeyUp (KeyCode.Space)) {
			ShowReadInstruction ();
			sMan.HideSign ();
			signpostDisplayed = false;
		}

	}


	void OnTriggerExit2D (Collider2D col)
	{
		if (iMan.IsActive() && col.gameObject.tag == "Player") {
			iMan.HideInstruction ();
			if (signpostDisplayed) {
				sMan.HideSign();
				signpostDisplayed = false;
			}
			signPostActive = false;
		}

	}

	void ChangeSortingOrder(Collider2D col) {
		int playerSortingOrder = col.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

		if (col.transform.position.y < transform.parent.position.y) {
			spriteRenderer.sortingOrder = playerSortingOrder - 1;
			signPostActive = true;
		} else {
			spriteRenderer.sortingOrder = playerSortingOrder + 1;
			signPostActive = false;
		}
	}

}
