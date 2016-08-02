using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
		copy.itemId = 0;
		copy.itemName = this.itemName;
		copy.itemDescription = this.itemDescription;
		copy.itemType = this.itemType;
		copy.containedResources = this.containedResources;
		copy.InitialiseItem();
		return copy;
	}

	//Returns the name formated into Title case with spaces, and any numbers removed
	public string GetNameFormatted ()
	{
		return ToTitleCase(itemName);
	}

	private string ToTitleCase (string input)
	{
		//add spaces
		string spaces = Regex.Replace (input, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");

		//remove any numbers;
		string digitsRemoved = Regex.Replace(spaces, @"[\d-]", string.Empty);
		return digitsRemoved;
	}

	public string GetTooltipInfo (string nameColor, int descriptionMaxWordsPerLine)
	{

		string formattedDescription = LineOverflow(itemDescription,descriptionMaxWordsPerLine);


		string resourcesString = string.Empty;
		if (containedResources != null && containedResources.Count > 0) {
			var temp = new HashSet<string> (containedResources);
			foreach (string s in temp) {
				resourcesString += ToTitleCase(s) + "\n";
			}
		}

		return string.Format("<color=" + nameColor + "><size=16>{0}</size></color>\n<i>{1}</i>\n<color=red>Recyclable Resources:</color>\n<i>{2}</i>", 
													GetNameFormatted(), formattedDescription, resourcesString);

	}

	private string LineOverflow (string input, int maxWordsPerLine)
	{
		string[] words = input.Split (' ');
		if (words.Length <= maxWordsPerLine) {
			return input;
		} 
		string output = string.Empty;
		for (int i = 0; i < words.Length; i++) {
			if (i == 0 || (i % maxWordsPerLine != 0)) {
				output += words[i] + " ";
			} 
			else {
				output += "\n" + words[i] + " ";
			} 
		}
		return output;
	}
}

public enum ItemType {
	RESOURCE,
	RUBBISH
}