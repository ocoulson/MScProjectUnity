using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	private ReadJSON reader;
	
	public InventoryItem[] resources;
	public InventoryItem[] rubbish;

	// Use this for initialization
	void Start () {
		reader = FindObjectOfType<ReadJSON>();

		resources = reader.GetResourceList();
		rubbish = reader.GetRubbishList();

	}


	public GameObject CreateRandomRubbishItem ()
	{
		int random = Random.Range(0, rubbish.Length);
		GameObject output = new GameObject ();
		output.AddComponent<Collectable> ();
		output.AddComponent<SpriteRenderer> ();
		InventoryItem item = rubbish[random].GetCopy();
		output.GetComponent<Collectable> ().item = item;
		output.GetComponent<SpriteRenderer>().sprite = item.sprite;
		output.name = item.itemName;
		output.tag = "Rubbish";
		output.layer = 17;
		output.GetComponent<SpriteRenderer>().sortingLayerName = "Rubbish";
		return output;
	}


}