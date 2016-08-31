using UnityEngine;
using System.Collections;

public class SortingGameSpawner : MonoBehaviour {

	private ItemDatabase itemDB;
	private float timer;
	// Use this for initialization
	void Start () {
		itemDB = FindObjectOfType<ItemDatabase>();
		timer = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		//Debug.Log(timer);
		if (timer > 3f) {
			SpawnRubbish();
			timer = 0;
		}
	}
	private void SpawnRubbish() {

		GameObject newRubbishItem = itemDB.CreateRandomRubbishItem();
		Debug.Log(newRubbishItem.GetComponent<Collectable>().item.ItemName);

		newRubbishItem.transform.position = transform.position;
		newRubbishItem.transform.SetParent(transform);
		Vector3 scale = newRubbishItem.transform.localScale;
		newRubbishItem.transform.localScale = scale * 10;

		newRubbishItem.AddComponent<Rigidbody2D>();

		Destroy(newRubbishItem.GetComponent<Collider2D>());
		PolygonCollider2D newCollider = newRubbishItem.AddComponent<PolygonCollider2D>();

	}
}
