using UnityEngine;
using System.Collections;

public class SpriteManager : MonoBehaviour {

	private RubbishSpawner rubbishSpawner;

	private SpriteSpawner[] spawners = null;

	void Awake () {
		spawners = transform.parent.GetComponentsInChildren<SpriteSpawner>();
		rubbishSpawner = transform.parent.GetComponentInChildren<RubbishSpawner>();
	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (spawners != null) {
			foreach (SpriteSpawner s in spawners) {
				s.Spawn ();
			}
		}
		if (!rubbishSpawner.rubbishSpawned) {
			rubbishSpawner.Spawn();
			rubbishSpawner.rubbishSpawned = true;
		}

	}
	void OnTriggerExit2D (Collider2D col)
	{
		if (spawners != null) {
			foreach (SpriteSpawner s in spawners) {
				s.Despawn ();

			}
		}
		if (rubbishSpawner.rubbishSpawned) {
			rubbishSpawner.Despawn();
			rubbishSpawner.rubbishSpawned = false;
		}
	}
}
