using UnityEngine;
using System.Collections;

public class AreaSystem : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			col.GetComponent<PlayerAdapter>().CurrentArea = GetComponentInParent<GameAreaAdapter>();
		}
	}
}
