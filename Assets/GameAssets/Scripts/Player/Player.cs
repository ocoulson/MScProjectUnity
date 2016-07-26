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

	void Start() {
		tools = new List<GameObject>();
		toolDisplay = FindObjectOfType<ToolDisplayManager>();
	}

	public void InitialiseInventory (int initialSize)
	{
		if (inventory == null) {
			inventory = new Inventory (initialSize);
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
