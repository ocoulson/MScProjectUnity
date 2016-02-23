using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {
	float speed;

	// Use this for initialization
	void Start () {
		speed = Random.Range(0.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.left * speed * Time.deltaTime;
	}
}
