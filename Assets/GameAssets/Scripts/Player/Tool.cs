using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tool : MonoBehaviour {
	public string toolName;
	public Sprite sprite;
	public Sprite icon;
	private Animator anim;

	private List<GameObject> interactionObjects;

	void Start() {
		anim = GetComponent<Animator>();
		gameObject.GetComponent<SpriteRenderer>().sprite = sprite;	
		interactionObjects = new List<GameObject>();
	}

	public InventoryItem Use ()
	{
		anim.SetTrigger ("UseTrigger");
		if (interactionObjects.Count > 0) {
			InventoryItem item = interactionObjects [0].GetComponent<Collectable> ().item;
			GameObject go = interactionObjects [0];
			interactionObjects.Remove (go);
			Destroy (go);
			return item;
		} else {
			throw new UnityException("No item in range of grabber");
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Rubbish") {
			interactionObjects.Add(col.gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "Rubbish") {
			if (interactionObjects.Contains(col.gameObject)) {
				interactionObjects.Remove(col.gameObject);
			}
		}
	}
}
