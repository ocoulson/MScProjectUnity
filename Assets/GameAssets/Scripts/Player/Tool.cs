using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Tool {
	public string toolName;

	private Sprite[] sprites;

	public Sprite[] Sprites {
		get {
			return sprites;
		}
		set {
			sprites = value;
		}
	}

	private Sprite icon;
	public Sprite Icon { get {return icon;} set { icon = value; } }



	public abstract InventoryItem Use();

	public abstract void OnTriggerEnter2DImpl(Collider2D col);
	public abstract void OnTriggerExit2DImpl(Collider2D col);
}
