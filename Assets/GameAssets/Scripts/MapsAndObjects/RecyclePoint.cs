using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ObserverPattern;

[System.Serializable]
public class RecyclePoint : Subject {

	private List<InventoryItem> recyclingItems;
	public List<InventoryItem> RecyclingItems { get {return recyclingItems;} set {recyclingItems = value;} }
	private string name;

	public string Name {get {return name;}}

	public int capacity;

	public int Count { get { return recyclingItems.Count;} }
	public bool IsFull { get { return Count >= capacity; } }
	public bool IsEmpty { get { return Count == 0; } }


	public RecyclePoint (string name, int capacity)
	{
		this.name = name;
		this.capacity = capacity;
		this.recyclingItems = new List<InventoryItem>();
	}

	public InventoryItem AddRubbishItem (InventoryItem rubbishItem)
	{
		if (IsFull) {
			return rubbishItem;
		}
		recyclingItems.Add(rubbishItem);
		return null;
	}

	public RecyclePoint Copy ()
	{
		RecyclePoint copy = new RecyclePoint (name, capacity);
		foreach (InventoryItem item in recyclingItems) {
			copy.AddRubbishItem(item.GetCopy());
		}
		return copy;
	}

}
