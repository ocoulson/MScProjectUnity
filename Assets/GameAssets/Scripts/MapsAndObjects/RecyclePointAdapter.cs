using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RecyclePointAdapter : MonoBehaviour {
	public string name;
	private RecyclePoint recyclePoint;

	public RecyclePoint RecyclePoint {
		get {
			return recyclePoint;
		}
		set {
			recyclePoint = value;
		}
	}

	private RecyclePointUI ui;
	private InventoryUIManager inventoryUI;
	private PlayerAdapter player;
	public Sprite empty;
	public Sprite full;
	// Use this for initialization
	void Start ()
	{
		ui = FindObjectOfType<RecyclePointUI>();
		inventoryUI = FindObjectOfType<InventoryUIManager>();
	}

	void Update ()
	{
		if (ui.IsFull) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = full;
		} else {
			gameObject.GetComponent<SpriteRenderer>().sprite = empty;
		}
	}

	//Method to add a list of items (ie from the player inventory) into the recycle point
	//The RecyclePoint UI object actually contains the items.
	//Any items added when the point is at capacity are returned to the player's inventory directly.
	public void DropOff (List<InventoryItem> input)
	{
		input.ForEach (item => Debug.Log (item.ItemName));

		for (int i = 0; i < input.Count; i++) {
			InventoryItem item = input [i];

			InventoryItem returned = recyclePoint.AddRubbishItem (item);	
			if (returned != null) {
				player.PutItemInInventory(returned);
			}	
		} 
		recyclePoint.Notify();
	
	}

	//A method which calls the DropOff method and puts in the player's inventory
	//Called by the Deposit button on the UI object
	public void GetPlayerDeposit ()
	{
		DropOff(player.DepositEntireInventory());
	}


	//TODO: When there are more than 1 Recycle Point, we need to ensure the UI displays the information for this Recycle point
	//Probably send this recyclePoint as an arguement to ShowUi
	void OnTriggerEnter2D (Collider2D col)
	{
		ui.ShowUI (recyclePoint);
		if (col.gameObject.tag == "Player") {
			player = col.GetComponent<PlayerAdapter> ();
			if (player.InventoryInitialised) {
				inventoryUI.ShowUI();
			}

		}
	}
	void OnTriggerExit2D(Collider2D col) {
		ui.HideUI();
		if (col.gameObject.tag == "Player") {
			inventoryUI.HideUI();
		}
	}
}
