using UnityEngine;
using System.Collections;

public class SmokeBehaviour : MonoBehaviour {
	private ParticleSystem smoke;

	// Use this for initialization
	void Start () {
		smoke = GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			smoke.playbackSpeed = 2;
		} else if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			smoke.playbackSpeed = 0.5f;
		} else {
			smoke.playbackSpeed = 1;
		}
	}
}
