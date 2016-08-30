using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class RecyclePointAdapter : MonoBehaviour {

	public string RecyclePointName;

	private RecyclePoint recyclePoint;
	public RecyclePoint RecyclePoint {
		get {
			return recyclePoint;
		}
		set {
			recyclePoint = value;
		}
	}
	private GameManager gameProgress;
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
		gameProgress = FindObjectOfType<GameManager>();
		player = FindObjectOfType<PlayerAdapter> ();
	}

	void Update ()
	{
		if (recyclePoint.IsFull) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = full;
			if (gameProgress.CheckPoints [recyclePoint.Name + "Full"] == CP_STATUS.UNTRIGGERED) {
				gameProgress.CheckPoints [recyclePoint.Name + "Full"] = CP_STATUS.TRIGGERED;
			}

		} else {
			gameObject.GetComponent<SpriteRenderer>().sprite = empty;
		}
	}

	//Method to add a list of items (ie from the player inventory) into the recycle point
	//The RecyclePoint object actually contains the items.
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
		if (player == null) {
			player = FindObjectOfType<PlayerAdapter>();
		}
		DropOff(player.DepositEntireInventory());
	}


	//TODO: When there are more than 1 Recycle Point, we need to ensure the UI displays the information for this Recycle point

	void OnTriggerEnter2D (Collider2D col)
	{
		ui.ShowUI (recyclePoint);
		if (col.gameObject.tag == "Player") {
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
