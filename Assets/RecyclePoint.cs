using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecyclePoint : MonoBehaviour {

	private RecyclePointUI ui;
	private InventoryUIManager inventoryUI;
	private Player player;
	// Use this for initialization
	void Start ()
	{
		ui = FindObjectOfType<RecyclePointUI>();
		inventoryUI = FindObjectOfType<InventoryUIManager>();
	}


	public void DropOff (List<InventoryItem> input)
	{
		input.ForEach(item => Debug.Log(item.itemName));

		Debug.Log("Ui contents quantity before: " + ui.TotalContentsQuantity());
		for (int i = 0; i < input.Count; i++) {
			InventoryItem item = input[i];

			ui.AddRubbishItem(item);		
		} 
		Debug.Log("Ui contents quantity after: " + ui.TotalContentsQuantity());
	
	}

	public void GetPlayerDeposit ()
	{
		DropOff(player.DepositEntireInventory());
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		ui.ShowUI ();
		if (col.gameObject.tag == "Player") {
			player = col.GetComponent<Player> ();
			if (player.inventoryInitialised) {
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
