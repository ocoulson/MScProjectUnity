using UnityEngine;
using System.Collections;

public class SpriteManager : MonoBehaviour {

	private Spawner[] spawners = null;

	void Awake () {
		spawners = transform.parent.GetComponentsInChildren<Spawner>();

	}
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (spawners != null) {
			foreach (Spawner s in spawners) {
				s.Spawn();
			}
		}

	}
	void OnTriggerExit2D (Collider2D col)
	{
		if (spawners != null) {
			foreach (Spawner s in spawners) {
				s.Despawn ();

			}
		}
	}
}
