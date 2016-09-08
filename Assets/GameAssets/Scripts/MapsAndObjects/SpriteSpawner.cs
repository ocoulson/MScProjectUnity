using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteSpawner : MonoBehaviour, Spawner {

	Transform[] spawnPoints;
	public string spawnPointTag;
	public GameObject prefab;

	public string[] names;

	// Use this for initialization
	void Start () {
		spawnPoints = GetSpawnPoints();
	}

	public void Spawn ()
	{
		foreach (Transform point in spawnPoints) {
			GameObject go = Instantiate (prefab, point.position, Quaternion.identity) as GameObject;
			go.transform.parent = gameObject.transform;
			if (names.Length > 0) {
				int random = Random.Range (0, names.Length);
				go.GetComponent<ReskinAnimation> ().SetSpriteSheetName (names [random]);
			}
		}
	}

	public void Despawn ()
	{
		for (int i = 0; i < transform.childCount; i++) {
			Destroy (transform.GetChild (i).gameObject);
		}

	}

	private Transform[] GetSpawnPoints ()
	{
		List<Transform> spawns = new List<Transform> ();
		GameObject container = transform.parent.parent.gameObject;
		SpawnPoint[] points = container.GetComponentsInChildren<SpawnPoint> ();

		foreach (SpawnPoint sp in points) {
			if (sp.tag == spawnPointTag) {
				spawns.Add(sp.transform);
			}
		}
		return spawns.ToArray();

	}
}
