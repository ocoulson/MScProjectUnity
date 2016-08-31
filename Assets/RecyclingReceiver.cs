using UnityEngine;
using System.Collections;

public class RecyclingReceiver : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		Destroy(col.gameObject);
	}
}
