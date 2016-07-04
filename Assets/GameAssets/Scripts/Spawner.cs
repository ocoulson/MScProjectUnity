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

	public void Spawn ()
	{
		foreach (GameObject point in spawnPoints) {
			GameObject go = Instantiate (prefab, point.transform.position, Quaternion.identity) as GameObject;
			go.transform.parent = gameObject.transform;
			if (names.Length > 0) {
				int random = Random.Range (0, names.Length);
				go.GetComponent<ReskinAnimation> ().SetSpriteSheetName (names [random]);
			}
		}
	}

	public bool Despawn ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			Destroy (transform.GetChild (i).gameObject);
		}
		if (transform.childCount == 0) {
			return true;
		} else {
			return false;
		}
	}

}
