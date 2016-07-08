using UnityEngine;
using System.Collections;

public class InteractionZoneManager : MonoBehaviour {

	public string instruction1 = "";
	public string instructionKey;
	public string instruciton2 = "";
	private SpriteRenderer spriteRenderer;
	private InstructionManager iManager;
	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
		iManager = GameObject.FindObjectOfType<InstructionManager>();
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		//show contextual interaction
		if (col.gameObject.name == "Player") {
			iManager.ShowInstruction(instruction1, instructionKey, instruciton2);
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.name == "Player") {
			ChangeSortingOrder(col);

			//Input response to Space press, ie. ("Its a huge pile of rubbish! I can't get past")
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			iManager.HideInstruction();
		} 
	}

	void ChangeSortingOrder(Collider2D col) {
		int playerSortingOrder = col.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

		if (col.transform.position.y < transform.position.y) {
			spriteRenderer.sortingOrder = playerSortingOrder - 1;
		} else {
			spriteRenderer.sortingOrder = playerSortingOrder + 1;
		}
	}


}
