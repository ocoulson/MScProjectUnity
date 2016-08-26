using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

[System.Serializable]
public class InventoryItem 
{
	private int itemId;
	private string itemName;
	private string itemDescription;
	private ItemType itemType;
	private List<String> containedResources;

	private Sprite sprite;

	public int ItemId {
		get { return itemId; }  
		set { itemId = value; }
	}

	public string ItemName {
		get { return itemName; }
		set { itemName = value; }
	}	

	public string ItemDescription {
		get { return itemDescription; }
		set { itemDescription = value; }
	}

	public ItemType ItemType {
		get { return itemType; }
		set { itemType = value; }
	}

	public List<String> ContainedResources {
		get { return containedResources; }
		set { containedResources = value; }
	}

	public Sprite Sprite {
		get { return sprite; }
		set { sprite = value; }
	}

	public InventoryItem ()
	{
		ContainedResources = new List<string>();
	}

	//Initialises the icon and list of contained resources after the object has been made from the json.
	public void InitialiseItem ()
	{
		Sprite = Resources.Load<Sprite> ("InventoryItems/" + ItemName + "Icon");
	}

	public InventoryItem GetCopy() {
		InventoryItem copy = new InventoryItem();
		copy.ItemId = 0;
		copy.ItemName = this.ItemName;
		copy.ItemDescription = this.ItemDescription;
		copy.ItemType = this.ItemType;
		copy.ContainedResources = this.ContainedResources;
		copy.InitialiseItem();
		return copy;
	}

	//Returns the name formated into Title case with spaces, and any numbers removed
	public string GetNameFormatted ()
	{
		return ToTitleCase(ItemName);
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

		string formattedDescription = LineOverflow(ItemDescription,descriptionMaxWordsPerLine);


		string resourcesString = string.Empty;
		if (ContainedResources != null && ContainedResources.Count > 0) {
			var temp = new HashSet<string> (ContainedResources);
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