using UnityEngine;
using System.Collections;

public class RubbishSpawner : MonoBehaviour {
	public int rubbishCount;
	public GameObject spawnArea;

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

		float halfBoxWidth = spawnArea.transform.localScale.x / 2;
		float halfBoxHeight = spawnArea.transform.localScale.y / 2;

		minX = spawnArea.transform.position.x - halfBoxWidth;
		maxX = spawnArea.transform.position.x + halfBoxWidth;

		minY = spawnArea.transform.position.y - halfBoxHeight;
		maxY = spawnArea.transform.position.y + halfBoxHeight;

		rubbishSpawned = false;
	}

	void Update ()
	{
		if (!rubbishSpawned) {
			SpawnRubbish();
			rubbishSpawned = true;
		}
	}

	Vector2 GetRandomPosition() {
		return new Vector2(Random.Range(minX,maxX), Random.Range(minY,maxY));
	}


	public void SpawnRubbish ()
	{
			for (int i = 0; i < rubbishCount; i++) {
				GameObject rubbishItem = itemDB.CreateRandomRubbishItem();
				rubbishItem.name = rubbishItem.name + i;
				rubbishItem.transform.parent = spawnArea.transform;
				rubbishItem.transform.position = GetRandomPosition();
			}
	}

}
