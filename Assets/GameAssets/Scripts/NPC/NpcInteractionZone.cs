using UnityEngine;
using System.Collections;

public class NpcInteractionZone : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private InstructionManager iManager;

	public bool playerInZone { get; private set; }

	void Start () {
		iManager = GameObject.FindObjectOfType<InstructionManager>();

		spriteRenderer = GetComponentInParent<SpriteRenderer>();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player" && !iManager.IsActive ()) {
			iManager.ShowInstruction("Press", "Space", "to talk");
			playerInZone = true;
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			ChangeSortingOrder(col);
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{	
		
		if (col.gameObject.tag == "Player") {
			iManager.HideInstruction();
			playerInZone = false;
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
