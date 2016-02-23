using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	float speed;
	private CloudSpawner spawner;
	float xBound;
	// Use this for initialization
	void Start () {
		speed = Random.Range(0.5f, 1.5f);

		spawner = GameObject.FindObjectOfType<CloudSpawner>();
		xBound = spawner.getMinX();

//		float zDistance = transform.position.z - Camera.main.transform.position.z;
//		Vector3 cameraX = Camera.main.ViewportToWorldPoint(new Vector3(0,0,zDistance));
//
//		xBound = cameraX.x - 5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.position += Vector3.left * speed * Time.deltaTime;

		if (transform.position.x < xBound - 5f) {
			Destroy(gameObject);
		}	
	}
}
