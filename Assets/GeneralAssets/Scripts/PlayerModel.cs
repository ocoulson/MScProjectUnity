using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ObserverPattern;


public class PlayerModel : Subject {

	private Vector2 currentPosition;

	public Vector2 CurrentPosition { get {return currentPosition;} set {currentPosition = value;} }

	private string name;

	public string Name { get {return name;} }

	private Gender gender;
	private string spriteName;

	public string SpriteName { get { return spriteName; } set { spriteName = value; } }

	private Inventory inventory;
	public Inventory Inventory {get {return inventory;}}
	public bool InventoryInitialised { get { return inventory != null; } }

	private List<Tool> tools;
	private Tool currentTool;

	private DialogueBlock[] thoughts;
	public DialogueBlock[] Thoughts { get {return thoughts;} set {thoughts = value; } }

	public PlayerModel(string name, Gender gender, string spriteName, Vector2 startPosition) {
		this.name = name;
		this.gender = gender;
		this.spriteName = spriteName;
		currentPosition = startPosition;
		tools = new List<Tool>();
	}

	public bool InitialiseInventory (int size)
	{
		if (inventory != null) {
			return false;
		}
		try {
			inventory = new Inventory (size);
		} catch (UnityException ex) {
			Debug.LogError(ex.Message);
		}
		return true;
	}

	public void IncreaseInventorySize (int newSize)
	{
		try {
			inventory.IncreaseCapacity (newSize);
		} catch (UnityException ex) {
			Debug.LogError(ex.Message);
		}
	}

	public void AddItem (InventoryItem item)
	{
		try {
			inventory.AddItem(item);
		} catch(UnityException ex) {
			Debug.LogError(ex.Message);
		}
	}

	public void RemoveItem (InventoryItem item)
	{
		try {
			inventory.RemoveItem (item);
		} catch (UnityException ex) {
			Debug.LogError(ex.Message);
		}
	}



	public bool AddTool (Tool newTool)
	{
		if (tools.Contains (newTool)) {
			return false;
		}
		tools.Add(newTool);
		if (currentTool == null)
			currentTool = newTool;
		return true;
	}

	public bool RemoveTool (Tool toBeRemoved)
	{
		if (!tools.Contains (toBeRemoved)) {
			return false;
		}
		tools.Remove(toBeRemoved);
		return true;
	}
}

public enum Gender {MALE,FEMALE}
