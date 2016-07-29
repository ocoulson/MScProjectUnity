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
		InventoryItem item = rubbish[random].GetCopy();
		return CreateRubbishItem(item);
	}

	public GameObject CreateRubbishItem(InventoryItem item) {

		GameObject output = new GameObject ();

		Collectable collectable = output.AddComponent<Collectable> ();
		SpriteRenderer spriteRenderer = output.AddComponent<SpriteRenderer> ();
		CircleCollider2D col = output.AddComponent<CircleCollider2D>();


		collectable.item = item;
		spriteRenderer.sprite = item.sprite;
		spriteRenderer.sortingLayerName = "Rubbish";
		output.name = item.itemName;
		output.tag = "Rubbish";
		output.layer = 17;

		col.radius = 0.07f;

		return output;
	}
}