using UnityEngine;
using System.Collections;

public class RubbishSpawner : MonoBehaviour {
	public int rubbishCount;

	private ItemDatabase itemDB;
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	public bool rubbishSpawned {get; set;}

	// Use this for initialization
	void Start ()
	{
		itemDB = FindObjectOfType<ItemDatabase>();

		float halfBoxWidth = transform.localScale.x / 2;
		float halfBoxHeight = transform.localScale.y / 2;

		minX = transform.position.x - halfBoxWidth;
		maxX = transform.position.x + halfBoxWidth;

		minY = transform.position.y - halfBoxHeight;
		maxY = transform.position.y + halfBoxHeight;

		rubbishSpawned = false;
	}


	Vector2 GetRandomPosition() {
		return new Vector2(Random.Range(minX,maxX), Random.Range(minY,maxY));
	}


	public void SpawnRubbish ()
	{
			for (int i = 0; i < rubbishCount; i++) {
				GameObject rubbishItem = itemDB.CreateRandomRubbishItem();
				rubbishItem.name = rubbishItem.name + i;
				rubbishItem.transform.parent = transform;
				rubbishItem.transform.position = GetRandomPosition();
			}
	}

	public void DespawnRubbish ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}
