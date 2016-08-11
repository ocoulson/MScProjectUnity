using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	private Transform target;
	public float followSpeed = 1f;
	Camera myCam;
	// Use this for initialization
	void Start () {
		myCam = GameObject.FindObjectOfType<Camera>();
		target = transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		myCam.orthographicSize = (Screen.height / 100f) / 4f;

		if (target) {
			myCam.transform.position = Vector3.Lerp(transform.position, target.position, followSpeed) + new Vector3(0,0,-10f);
		}
	}
}
