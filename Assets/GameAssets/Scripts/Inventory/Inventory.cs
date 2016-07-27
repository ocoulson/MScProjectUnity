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
			throw new ArgumentException ("New inventory size is less than or equal to current size");
		} else {
			size = newSize;
		}
	}

	public void AddItem (InventoryItem item)
	{
		if (items.Count < size) {
			items.Add (item);
		} else {
			throw new ArgumentException("Inventory Full");
		} 
	}

	public InventoryItem RemoveItem (InventoryItem item)
	{	
		if (items.Count == 0 || !items.Contains (item)) {
			throw new ArgumentException("Item not in inventory");
		} else {
			int index = items.IndexOf (item);
			InventoryItem output = items [index];
			items.Remove (item);
			return output;
		}
	}

}