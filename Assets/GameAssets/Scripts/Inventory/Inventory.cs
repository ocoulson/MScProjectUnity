using UnityEngine;
using System;
using System.Collections.Generic;

public class Inventory
{
	public Int32 Size {get; private set;}

	private List<InventoryItem> items;
	public List<InventoryItem> Items { 
		get { return items; } 
		private set { items = value; }
	}

	public bool IsEmpty {
		get { return Count <= 0;}
	}
	public bool IsFull {
		get { return Count >= Size;}
	}

	public int Count {
		get { return Items.Count;}
	}

	public Inventory (int initialSize)
	{
		Size = initialSize;
		Items = new List<InventoryItem>();

	}
	public void IncreaseCapacity (int newSize)
	{
		if (newSize <= Size) {
			throw new UnityException ("New inventory size is less than or equal to current size");
		} else {
			Size = newSize;
		}
	}

	public void AddItem (InventoryItem item)
	{
		if (!IsFull) {
			Items.Add (item);
		} else {
			throw new UnityException("Inventory Full");
		} 
	}

	public InventoryItem RemoveItem (InventoryItem item)
	{	
		if (IsEmpty || !Items.Contains (item)) {
			throw new UnityException("Item not in inventory");
		} else {
			int index = Items.IndexOf (item);
			InventoryItem output = Items [index];
			Items.Remove (item);
			return output;
		}
	}

	public List<InventoryItem> RemoveAll ()
	{
		if (IsEmpty) {
			throw new UnityException("Can't remove from empty inventory");
		}
		List<InventoryItem> output = Items;
		Items = new List<InventoryItem>();
		return output;
	}


	public override string ToString ()
	{
		string output = "";
		foreach (InventoryItem item in Items) {
			output += item.ItemName + ", ";
		}
		return output.Substring(0, output.Length-2);
	}
}