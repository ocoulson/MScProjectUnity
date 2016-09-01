using UnityEngine;
using System.Collections;

public class InteriorArea : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			GameObject.FindGameObjectWithTag("Smoke").SetActive(false);
		}
	}
	void OnTriggerExit2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			GameObject.FindGameObjectWithTag("Smoke").SetActive(true);
		}
	}
}
