using UnityEngine;
using System.Collections;

public class NpcInteractionZone : MonoBehaviour {

	
	private NpcController controller;
	private SpriteRenderer spriteRenderer;
	private InstructionManager iManager;

	void Start () {
		iManager = GameObject.FindObjectOfType<InstructionManager>();
		controller = GetComponentInParent<NpcController>();
		spriteRenderer = GetComponentInParent<SpriteRenderer>();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		//Call the interaction zone method in the NPC controller.
		controller.InteractionZoneEnter (col);

		if (!iManager.IsActive () && col.gameObject.name == "Player") {
			iManager.ShowInstruction("Press", "Space", "to talk");
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			ChangeSortingOrder(col);
		}
		//Call the handler method of the NPC controller
		controller.InteractionZoneStay(col);
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			iManager.HideInstruction();
		} 
	}

	//Change the sorting layer order of the NPC depending on where the player is (below or above it)
	//to give a 3d impression (i.e. npc rendered behind if player is below, and vice versa)
	void ChangeSortingOrder(Collider2D col) {
		int playerSortingOrder = col.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

		if (col.transform.position.y < transform.position.y) {
			spriteRenderer.sortingOrder = playerSortingOrder - 1;
		} else {
			spriteRenderer.sortingOrder = playerSortingOrder + 1;
		}
	}





}
