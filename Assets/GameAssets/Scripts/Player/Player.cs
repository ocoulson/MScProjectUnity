using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ObserverPattern;

[System.Serializable]
public class Player : Subject {

	private Vector2Serializable currentPosition;

	public Vector2 CurrentPosition { get {return currentPosition.Vector2;} set {currentPosition.Vector2 = value;} }

	private string name;
	public string Name { get {return name;} }

	private Gender gender;
	public Gender Gender { get { return gender; } }

	private string spriteName;
	public string SpriteName { get { return spriteName; } set { spriteName = value; } }

	private Inventory inventory;
	public Inventory Inventory {get {return inventory;}}
	public bool InventoryInitialised { get { return Inventory != null; } }

	private List<Tool> tools;
	public List<Tool> Tools {
		get {return tools;}
	}

	private Tool currentTool;
	public Tool CurrentTool {
		get {return currentTool;}
		private set {currentTool = value;}
	}

	private DialogueBlock[] thoughts;
	public DialogueBlock[] Thoughts { get {return thoughts;} set {thoughts = value; } }

	private GameArea currentArea;
	public GameArea CurrentArea { get { return currentArea; } set { currentArea = value; } }

	public Player(string name, Gender gender, string spriteName, Vector2 startPosition) {
		this.name = name;
		this.gender = gender;
		this.spriteName = spriteName;
		currentPosition = new Vector2Serializable(startPosition);
		tools = new List<Tool>();
	}

	public void InitialiseInventory (int size)
	{
		if (inventory != null) {
			throw new UnityException("Inventory already initialised"); 
		}
		try {
			inventory = new Inventory (size);
		} catch (UnityException ex) {
			Debug.LogError(ex.Message);
		}
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
		inventory.AddItem(item);
		
	}

	public InventoryItem RemoveItem (InventoryItem item)
	{
		return inventory.RemoveItem (item);
		
	}

	public bool AddTool (Tool newTool)
	{
		if (tools.Contains (newTool)) {
			return false;
		}
		tools.Add (newTool);
		if (currentTool == null) {
			currentTool = newTool;
			Notify();
		}
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

	public void SetCurrentTool(int index) {
		if (index > Tools.Count - 1) {
			throw new IndexOutOfRangeException("Tools length is less than index: " + index);
		} else {
			CurrentTool = Tools[index];
			Notify();
		}
	}
	public void SetCurrentTool (string name)
	{
		Tool newTool = Array.Find (Tools.ToArray (), tool => tool.toolName == name);
		if (newTool != null) {
			CurrentTool = newTool;
		} else {
			throw new ArgumentException("Tool with name: '"+ name +"' does not exist in the player");
		}
	}

	public Player Copy ()
	{
		Player copy = new Player (Name, Gender, SpriteName, CurrentPosition);
		if (InventoryInitialised) {
			copy.inventory = Inventory.Copy ();
		}
		foreach (Tool tool in Tools) {
			copy.Tools.Add (tool.Copy ());
		}
		if (currentTool != null) {
			copy.currentTool = currentTool.Copy ();
		}

		if (Thoughts != null) {
			List<DialogueBlock> thoughtsCopy = new List<DialogueBlock> ();
			foreach (DialogueBlock block in Thoughts) {
				thoughtsCopy.Add(block.Copy());
			}
			copy.Thoughts = thoughtsCopy.ToArray();
		}
		return copy;
	}


}

public enum Gender {MALE,FEMALE}
