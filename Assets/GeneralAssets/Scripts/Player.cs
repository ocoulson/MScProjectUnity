using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ObserverPattern;


public class Player : Subject {

	private Vector2 currentPosition;

	private string name;

	private Gender gender;

	private string spriteName;

	private Inventory inventory;

	private List<Tool> tools;
	private Tool currentTool;

	public Player(string name, Gender gender, string spriteName, Vector2 startPosition) {
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
		inventory = new Inventory(size);
		return true;
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
