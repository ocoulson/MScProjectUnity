using UnityEngine;
using System;

public abstract class InventoryItem
{
	public string iconPath;
	public string spritePath;


	public Texture2D icon;
	public Texture2D sprite;

	public InventoryItem ()
	{
	}

	public void InitialiseImages() {
		icon = Resources.Load<Texture2D>(iconPath);
		sprite = Resources.Load<Texture2D>(spritePath);
	}
}


