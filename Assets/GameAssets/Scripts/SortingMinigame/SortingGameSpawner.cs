using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortingGameSpawner : MonoBehaviour {
	public float frequency;
	private ItemDatabase itemDB;
	private float timer;
	private List<InventoryItem> toBeSpawned;

	public int Remaining {
		get { return toBeSpawned.Count; }
	}
	// Use this for initialization
	void Start () {
		toBeSpawned = new List<InventoryItem>();
		itemDB = FindObjectOfType<ItemDatabase>();
		timer = 0;
		for (int i = 0; i<20; i++) {
			toBeSpawned.Add(itemDB.rubbish[Random.Range(0, itemDB.rubbish.Length)].GetCopy());
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		//Debug.Log(timer);
		if (timer > frequency && toBeSpawned.Count > 0) {
			SpawnRubbish();
			timer = 0;
		}
	}
	private void SpawnRubbish() {

		GameObject newRubbishItem = itemDB.CreateItemGameObject(toBeSpawned[0]);
		toBeSpawned.Remove(toBeSpawned[0]);
		Debug.Log(newRubbishItem.GetComponent<Collectable>().item.ItemName);

		newRubbishItem.transform.position = transform.position;
		newRubbishItem.transform.SetParent(transform);
		Vector3 scale = newRubbishItem.transform.localScale;
		newRubbishItem.transform.localScale = scale * 10;

		newRubbishItem.AddComponent<Rigidbody2D>();

		Destroy(newRubbishItem.GetComponent<Collider2D>());
		newRubbishItem.AddComponent<PolygonCollider2D>();

	}
	public void ReturnMissedRubbish(InventoryItem missed) {
		toBeSpawned.Add(missed);
	}
}
