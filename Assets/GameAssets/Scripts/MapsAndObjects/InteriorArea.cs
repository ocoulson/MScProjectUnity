using UnityEngine;
using System.Collections;

public class InteriorArea : AreaSystem {

	void Start() {
		Area = new GameArea(gameObject.name);

	}


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
