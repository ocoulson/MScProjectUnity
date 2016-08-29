using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Tool {
	public string toolName;

	public abstract InventoryItem Use();
	public abstract Tool Copy();

	public abstract void OnTriggerEnter2DImpl(Collider2D col);
	public abstract void OnTriggerExit2DImpl(Collider2D col);
}
