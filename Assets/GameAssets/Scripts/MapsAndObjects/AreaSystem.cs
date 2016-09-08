using UnityEngine;
using System.Collections;

public class AreaSystem : MonoBehaviour {
	
	private GameArea area;

	public GameArea Area {
		get {
			return area;
		}
		protected set { 
			area = value;
		}
	}
	private GameObject smokeObject;

	public GameObject smokeLocation;

	void Start() {
		area = new GameArea(gameObject.name);
		smokeObject = GameObject.FindGameObjectWithTag("Smoke");
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			col.GetComponent<PlayerAdapter>().CurrentArea = GetComponentInParent<AreaSystem>();
			smokeObject.transform.position = smokeLocation.transform.position;
		}
	}
}
