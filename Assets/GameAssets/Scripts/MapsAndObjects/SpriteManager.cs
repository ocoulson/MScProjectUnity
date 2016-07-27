using UnityEngine;
using System.Collections;

public class SpriteManager : MonoBehaviour {

	private RubbishSpawner rubbishSpawner;

	private Spawner[] spawners = null;

	void Awake () {
		spawners = transform.parent.GetComponentsInChildren<Spawner>();
		rubbishSpawner = transform.parent.GetComponentInChildren<RubbishSpawner>();
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (spawners != null) {
			foreach (Spawner s in spawners) {
				s.Spawn ();
			}
		}
		if (!rubbishSpawner.rubbishSpawned) {
			rubbishSpawner.SpawnRubbish();
			rubbishSpawner.rubbishSpawned = true;
		}

	}
	void OnTriggerExit2D (Collider2D col)
	{
		if (spawners != null) {
			foreach (Spawner s in spawners) {
				s.Despawn ();

			}
		}
		if (rubbishSpawner.rubbishSpawned) {
			rubbishSpawner.DespawnRubbish();
			rubbishSpawner.rubbishSpawned = false;
		}
	}
}
