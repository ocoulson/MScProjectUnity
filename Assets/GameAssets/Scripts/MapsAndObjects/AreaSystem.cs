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
	private GameObject particleSystemObject;

	public GameObject particleSystemLocation;

	void Start() {
		area = new GameArea(gameObject.name);
		particleSystemObject = GameObject.FindGameObjectWithTag("Smoke");
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			col.GetComponent<PlayerAdapter>().CurrentArea = GetComponentInParent<AreaSystem>();
			particleSystemObject.transform.position = particleSystemLocation.transform.position;
		}
	}
}
