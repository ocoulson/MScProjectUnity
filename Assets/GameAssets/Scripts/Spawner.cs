using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	GameObject[] spawnPoints;
	public string spawnPointTag;
	public GameObject prefab;
	public string[] names;

	// Use this for initialization
	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag(spawnPointTag);
		Spawn();

	}

	public void Spawn () {
		foreach (GameObject point in spawnPoints) {
			int random = Random.Range(0, names.Length);
			GameObject go = Instantiate(prefab, point.transform.position, Quaternion.identity) as GameObject;
			go.transform.parent = gameObject.transform;
			go.GetComponent<ReskinAnimation>().SetSpriteSheetName(names[random]);
		}
	}

}
