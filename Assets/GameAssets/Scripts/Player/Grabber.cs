using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Grabber : Tool {

	[System.NonSerialized]
	private List<GameObject> interactionObjects;

	public List<GameObject> InteractionObjects { get { return interactionObjects; } set { interactionObjects = value;}}

	public Grabber ()
	{
		//Instantiate Tool fields
		toolName = "Grabber1";


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

	public override Tool Copy ()
	{
		return new Grabber();
	}

	//Methods used by the ToolAdapter OnTriggerEnter2D / Exit2D methods 
	public override void OnTriggerEnter2DImpl (Collider2D col)
	{
		if (col.tag == "Rubbish") {
			//because field is not serialised, need to reinitialise it after deserialisation
			if (interactionObjects == null) {
				interactionObjects = new List<GameObject>();
			}
			interactionObjects.Add(col.gameObject);
		}
	}

	public override void OnTriggerExit2DImpl (Collider2D col)
	{
		
		if (col.tag == "Rubbish") {
			//because field is not serialised, need to reinitialise it after deserialisation
			if (interactionObjects == null) {
				interactionObjects = new List<GameObject>();
			}
			if (interactionObjects.Contains(col.gameObject)) {
				interactionObjects.Remove(col.gameObject);
			}
		}
	}

	#endregion
}
