using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ItemDatabase : MonoBehaviour {
	private ReadJson reader;
	
	public InventoryItem[] resources;
	public InventoryItem[] rubbish;

	private Int32 lastId;
	// Use this for initialization
	void Start () {
		lastId = 1;
		reader = FindObjectOfType<ReadJson>();

		resources = reader.GetResourceList();
		rubbish = reader.GetRubbishList();

	}

	public Stack<InventoryItem> GetAllRubbishItems ()
	{
		Stack<InventoryItem> output = new Stack<InventoryItem> ();
		foreach (InventoryItem item in rubbish) {
			output.Push(item.GetCopy());
		}
		return output;
	}

	public GameObject CreateRandomRubbishItem ()
	{
		int random = UnityEngine.Random.Range(0, rubbish.Length);
		InventoryItem item = rubbish[random].GetCopy();
		item.ItemId = lastId;
		lastId++;
		return CreateItemGameObject(item);
	}

	public GameObject CreateItemGameObject(InventoryItem item) {

		GameObject output = new GameObject ();

		Collectable collectable = output.AddComponent<Collectable> ();
		SpriteRenderer spriteRenderer = output.AddComponent<SpriteRenderer> ();
		CircleCollider2D col = output.AddComponent<CircleCollider2D>();


		collectable.item = item;
		spriteRenderer.sprite = item.Sprite;
		spriteRenderer.sortingLayerName = "Rubbish";
		output.name = item.ItemName;
		output.tag = "Rubbish";
		output.layer = 17;

		col.radius = 0.07f;

		return output;
	}


}