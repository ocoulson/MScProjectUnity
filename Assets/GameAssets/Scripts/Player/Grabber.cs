using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grabber : Tool {

	private List<GameObject> interactionObjects;

	public Grabber ()
	{
		toolName = "Grabber1";

		interactionObjects = new List<GameObject>();

		Sprites = Resources.LoadAll<Sprite>("Equipment/" + toolName);

		Icon = Resources.Load<Sprite>("Equipment/"+ toolName+ "Icon"); 
	}

	public override InventoryItem Use ()
	{
		if (interactionObjects.Count > 0) {
			InventoryItem item = interactionObjects [0].GetComponent<Collectable> ().item;
			GameObject go = interactionObjects [0];
			interactionObjects.Remove (go);
			GameObject.Destroy (go);
			return item;
		} else {
			throw new UnityException("No item in range of grabber");
		}
	}



	#region implemented abstract members of Tool

	public override void OnTriggerEnter2DImpl (Collider2D col)
	{
		if (col.tag == "Rubbish") {
			interactionObjects.Add(col.gameObject);
		}
	}

	public override void OnTriggerExit2DImpl (Collider2D col)
	{
		if (col.tag == "Rubbish") {
			if (interactionObjects.Contains(col.gameObject)) {
				interactionObjects.Remove(col.gameObject);
			}
		}
	}

	#endregion
}
