using UnityEngine;
using System;
using System.Collections.Generic;

public class InventoryItem 
{
	public int itemId;
	public string itemName;
	public string itemDescription;
	public ItemType itemType;

	public List<String> containedResources;
	public List<InventoryItem> containedResourceItems;

	public Sprite sprite;

	public InventoryItem ()
	{
		containedResources = new List<string>();
		containedResourceItems = new List<InventoryItem>();

	}

	//Initialises the icon and list of contained resources after the object has been made from the json.
	public void InitialiseItem ()
	{
		sprite = Resources.Load<Sprite> ("InventoryItems/" + itemName + "Icon");


		ItemDatabase itemDatabase = GameObject.FindObjectOfType<ItemDatabase> ();

		foreach (string resourceName in containedResources) {
			InventoryItem resource = Array.Find<InventoryItem>(itemDatabase.resources, item => item.itemName == resourceName).GetCopy();
			containedResourceItems.Add(resource);
		}
	}

	public InventoryItem GetCopy() {
		InventoryItem copy = new InventoryItem();
		copy.itemId = this.itemId;
		copy.itemName = this.itemName;
		copy.itemDescription = this.itemDescription;
		copy.itemType = this.itemType;
		copy.containedResources = this.containedResources;
		copy.InitialiseItem();
		return copy;
	}
}

public enum ItemType {
	RESOURCE,
	RUBBISH
}