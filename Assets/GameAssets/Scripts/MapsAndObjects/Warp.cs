using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {
	public Transform target;

	float timeDelay = 1f;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			//Debug.Log ("Triggered: " + target.name);

			StartCoroutine(WarpTime(col));

			col.GetComponent<PlayerGameObject>().currentArea = target.parent.parent.gameObject;
		}

	}

	IEnumerator WarpTime(Collider2D col) {
		PlayerMovement movement = col.GetComponent<PlayerMovement>();
		movement.DisableMovement();

		yield return new WaitForSeconds(timeDelay);

		col.transform.position = target.position;
		movement.EnableMovement();
	}


}
