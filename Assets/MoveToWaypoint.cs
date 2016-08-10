using UnityEngine;
using System.Collections;

public class MoveToWaypoint : MonoBehaviour {
	public GameObject waypoint;
	private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position != waypoint.transform.position) {
			Vector2 velocity = (waypoint.transform.position - transform.position) * 2;
			rBody.velocity = velocity;
		} 
	}
}
