using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using ObserverPattern;

public class Inventory : Subject
{
	public int Size {get; private set;}

	private List<InventoryItem> items;
	public List<InventoryItem> Items { 
		get { return items; } 
		private set { items = value; }
	}

	public int Count {
		get { return Items.Count;}
	}

	public bool IsEmpty {
		get { return Count <= 0;}
	}
	public bool IsFull {
		get { return Count >= Size;}
	}



	public Inventory (int initialSize)
	{
		if (initialSize <= 0) {
			throw new UnityException("Initial inventory size must be greater than 0");
		}
		Size = initialSize;
		Items = new List<InventoryItem>();

	}
	public void IncreaseCapacity (int newSize)
	{
		if (newSize <= Size) {
			throw new UnityException ("New inventory size must be greater than current size: " + Size);
		} else {
			Size = newSize;
		}
		Notify();
	}

	public void AddItem (InventoryItem item)
	{
		if (!IsFull) {
			Items.Add (item);
		} else {
			throw new UnityException("Inventory Full");
		} 
		Notify();
	}

	public bool Contains (InventoryItem item)
	{
		return Items.Contains(item);
	}

	public InventoryItem RemoveItem (InventoryItem item)
	{	
		if (IsEmpty || !Contains (item)) {
			throw new UnityException("Item not in inventory");
		} else {
			int index = Items.IndexOf (item);
			InventoryItem output = Items [index];
			Items.Remove (item);
			Notify();
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
		Notify();
		return output;
	}


	public override string ToString ()
	{
		StringBuilder builder = new StringBuilder ();

		for (int i = 0; i < Items.Count; i++) {
			if (i == Items.Count - 1) {
				builder.Append (items [i].ItemName);
			} else {
				builder.Append(items[i].ItemName + ", ");
			}
		}
	
		return builder.ToString();
	}
}