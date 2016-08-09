using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour {

	public GameObject toolSlot;
	public GameObject wearableSlot;
	public GameObject wearable {get; private set;}

	public GameObject currentTool {get; private set;}
	public List<GameObject> tools { get; private set; }

	public Inventory inventory { get; private set; }

	private ToolDisplayManager toolDisplay;
	private InventoryUIManager inventoryUiManager;
	private ThoughtBubbleManager thoughtBubbleManager;

	public bool inventoryInitialised { get; private set; }
	private bool toolEquipped { get { return currentTool != null; } }
	public  GameObject currentArea {get; set;}

	void Start() {
		inventoryInitialised = false;
		tools = new List<GameObject>();
		toolDisplay = FindObjectOfType<ToolDisplayManager>();
		inventoryUiManager = FindObjectOfType<InventoryUIManager>();
		thoughtBubbleManager = FindObjectOfType<ThoughtBubbleManager>();
	}

	void Update ()
	{
		if (toolEquipped && Input.GetKeyDown(KeyCode.E)) {
			Tool tool = GetComponentInChildren<Tool> ();
			InventoryItem pickedUp = null;

			try {
				pickedUp = tool.Use ();
			} catch (UnityException ex) {
				Debug.Log (ex.Message);
			}

			if (pickedUp != null) {

				try {
					AddItem (pickedUp);
				} catch (UnityException ex) {
					Debug.Log (ex.Message);
					StartCoroutine (InventoryFullEvent (pickedUp));
				}

			}

		}
		if (inventoryInitialised && Input.GetKeyDown (KeyCode.I)) {
			inventoryUiManager.Toggle ();
		}
		if (inventoryInitialised && Input.GetKeyDown (KeyCode.P)) {
			Debug.Log(inventory); 
		}
		if (inventoryInitialised && Input.GetKey(KeyCode.RightShift) && Input.GetKeyUp(KeyCode.O)) {
			DropAllItems();
		}

	}

	IEnumerator InventoryFullEvent(InventoryItem item) {
		GetComponent<PlayerMovement>().DisableMovement();
		thoughtBubbleManager.ShowThoughtBubble("My backpack is full, I can't carry this");
		yield return new WaitForSeconds(1f);

		thoughtBubbleManager.HideThoughtBubble();
		DropItem(item);
		GetComponent<PlayerMovement>().EnableMovement();
	}

	public List<InventoryItem> DepositEntireInventory ()
	{
		List<InventoryItem> output = inventory.RemoveAll();
		inventoryUiManager.UpdateInventoryUi();
		return output;
	}

	public void AddMultipleItems (List<InventoryItem> input)
	{
		foreach (InventoryItem item in input) {
			try {
				inventory.AddItem (item);
			} catch (UnityException ex) {
				Debug.Log (ex.Message);
				StartCoroutine (InventoryFullEvent (item));
			}
		}
	}

	public void DropItem(InventoryItem item) {
		if (inventory.Items.Contains(item)) {
			inventory.RemoveItem(item);
		}
		GameObject rubbishItem = GameObject.FindObjectOfType<ItemDatabase>().CreateItemGameObject(item);
		rubbishItem.transform.parent = currentArea.GetComponentInChildren<RubbishSpawner>().transform;
		float dropRadius = 0.2f;
		rubbishItem.transform.position = gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-dropRadius,dropRadius),
										 UnityEngine.Random.Range(-dropRadius,dropRadius));
	}

	public void DropAllItems ()
	{
		InventoryItem[] items = inventory.Items.ToArray();
		foreach (InventoryItem item in items) {
			DropItem(item);
		}
		inventoryUiManager.UpdateInventoryUi();
	}

	public void InitialiseInventory (int initialSize)
	{
		if (inventory == null) {
			inventory = new Inventory (initialSize);
			inventoryUiManager.LinkInventoryToUI(inventory);
			inventoryInitialised = true;
		} else {
			Debug.LogError("Inventory already initilalised");
		}
	}

	public void IncreaseInventorySize (int newSize)
	{
		try {
			inventory.IncreaseCapacity (newSize);
		} catch (ArgumentException ex) {
			Debug.LogError(ex.Message);
		}
	}

	public void AddItem (InventoryItem item)
	{
		try {
			inventory.AddItem(item);
		} catch(ArgumentException ex) {
			Debug.LogError(ex.Message);
		}
		inventoryUiManager.UpdateInventoryUi();
	}

	public void SetWearable (GameObject newWearable)
	{
		wearable = newWearable;
		SetParentAndPosition(wearable.transform, wearableSlot.transform);
	}

	public void AddTool (GameObject tool)
	{
		if (!tools.Contains (tool)) {
			tools.Add (tool);
			if (currentTool == null) {
				SetCurrentTool (tools.IndexOf (tool));
			}
		}
	}

	public void SetCurrentTool (int index)
	{
		if (index > tools.Count - 1) {
			Debug.Log ("Invalid tool choice");
		} else {
			currentTool = tools [index];
			toolSlot.transform.DetachChildren ();
			SetParentAndPosition (currentTool.transform, toolSlot.transform);

			toolDisplay.ShowToolImage ();
			toolDisplay.SetToolImage (currentTool.GetComponent<Tool> ().icon);
		}
	}

	private void SetParentAndPosition(Transform target, Transform holder) {
		target.parent = holder;
		target.position = holder.position;
		target.rotation = holder.rotation;
	}

}
