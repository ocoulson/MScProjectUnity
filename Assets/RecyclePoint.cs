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

		for (int i = 0; i < input.Count; i++) {
			InventoryItem item = input[i];
			ui.AddRubbishItem(item);		
			input.Remove (input [i]);
		} 
		

	}

	public void GetPlayerDeposit ()
	{
		DropOff(player.DepositRubbish());
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		ui.ShowUI ();
		if (col.gameObject.tag == "Player") {
			player = col.GetComponent<Player>();
			inventoryUI.ShowUI();
		}
	}
	void OnTriggerExit2D(Collider2D col) {
		ui.HideUI();
		if (col.gameObject.tag == "Player") {
			inventoryUI.HideUI();
		}
	}
}
