using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {
	public Transform target;

	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log("Triggered");
		col.transform.position = target.position;
	}

}
