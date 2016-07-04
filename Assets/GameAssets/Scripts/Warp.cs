using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {
	public Transform target;
	private Spawner[] currentAreaSpawners;
	private Spawner[] targetAreaSpawners;
	private GameObject parentAreaContainer;
	private GameObject targetAreaContainer;

	void Start() {
		parentAreaContainer = transform.parent.parent.gameObject;
		targetAreaContainer = target.parent.parent.gameObject;
		currentAreaSpawners = parentAreaContainer.GetComponentsInChildren<Spawner>();
		Debug.Log("current spawners: " + currentAreaSpawners.Length);
		targetAreaSpawners = targetAreaContainer.GetComponentsInChildren<Spawner>();
		Debug.Log("new spawners: " + targetAreaSpawners.Length);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		Debug.Log ("Triggered: " + target.name);
		col.transform.position = target.position;

		foreach (Spawner s in currentAreaSpawners) {
			s.Despawn ();
		}
		foreach (Spawner s in targetAreaSpawners) {
			s.Spawn();
		}
	}

}
