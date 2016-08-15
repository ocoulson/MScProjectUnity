using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grabber : Tool {
	private static Grabber instance;

	//TODO: Investigate synchronisation with regards to unity
	public static Grabber Instance {
		get {
			if (instance == null) {
				instance = new Grabber();
			}
			return instance;
		}
	}

	private List<GameObject> interactionObjects;

	public List<GameObject> InteractionObjects { get { return interactionObjects; } set { interactionObjects = value;}}

	private Grabber ()
	{
		//Instantiate Tool fields
		toolName = "Grabber1";
		Sprites = Resources.LoadAll<Sprite>("Equipment/" + toolName);
		Icon = Resources.Load<Sprite>("Equipment/"+ toolName+ "Icon"); 

		//Instantiate Grabber field
		interactionObjects = new List<GameObject>();
	}

	#region implemented abstract members of Tool
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

	//Methods used by the ToolAdapter OnTriggerEnter2D / Exit2D methods 
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
