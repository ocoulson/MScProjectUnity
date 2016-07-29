using UnityEngine;
using System;
using System.Collections.Generic;

public class Inventory
{
	public Int32 size {get; set;}

	public List<InventoryItem> items { get; set; }

	public Inventory (int initialSize)
	{
		size = initialSize;
		items = new List<InventoryItem>();

	}
	public void IncreaseCapacity (int newSize)
	{
		if (newSize <= size) {
			throw new UnityException ("New inventory size is less than or equal to current size");
		} else {
			size = newSize;
		}
	}

	public void AddItem (InventoryItem item)
	{
		if (items.Count < size) {
			items.Add (item);
		} else {
			throw new UnityException("Inventory Full");
		} 
	}

	public InventoryItem RemoveItem (InventoryItem item)
	{	
		if (items.Count == 0 || !items.Contains (item)) {
			throw new UnityException("Item not in inventory");
		} else {
			int index = items.IndexOf (item);
			InventoryItem output = items [index];
			items.Remove (item);
			return output;
		}
	}


	public override string ToString ()
	{
		string output = "";
		foreach (InventoryItem item in items) {
			output += item.itemName + ", ";
		}
		return output.Substring(0, output.Length-2);
	}
}