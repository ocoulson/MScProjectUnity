using UnityEngine;
using System.Collections;

public class InteractionZoneManager : MonoBehaviour {

	public string instruction1 = "";
	public string instructionKey;
	public string instruction2 = "";

	public string playerInteractionText;

	private SpriteRenderer spriteRenderer;
	private InstructionManager iManager;
	private PlayerInteractionManager playerInteraction = null;
	private bool playerInZone;

	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
		iManager = GameObject.FindObjectOfType<InstructionManager>();

	}

	void Update ()
	{
		if (playerInZone && Input.GetKeyUp (KeyCode.Space)) {
			Debug.Log ("Space pressed");
			if (!playerInteraction.IsThoughtBubbleActive ()) {
				
				playerInteraction.DisplayThoughtBubble (playerInteractionText);
			} else if (playerInteraction.IsThoughtBubbleActive ()) {
				
				playerInteraction.HideThoughtBubble ();
			}
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			if (!iManager.IsActive ()) {
				iManager.ShowInstruction (instruction1, instructionKey, instruction2);
			}
			playerInZone = true;
			playerInteraction = col.gameObject.GetComponent<PlayerInteractionManager> ();
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			ChangeSortingOrder (col);
		
		}
	}

	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			if (iManager.IsActive ()) {
				iManager.HideInstruction ();
			}

			if (playerInteraction.IsThoughtBubbleActive ()) {
				playerInteraction.HideThoughtBubble();
			}
			playerInZone = false;
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
