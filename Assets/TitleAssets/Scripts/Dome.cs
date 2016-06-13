using UnityEngine;
using System.Collections;

public class Dome : MonoBehaviour {
	AudioSource aSource;
	public AudioClip[] sounds;

	void Awake ()
	{
		aSource = gameObject.GetComponent<AudioSource>();
		aSource.clip = sounds[0];
		aSource.Play();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.transform.tag == "BottomCollider") {
			aSource.clip = sounds [1];
			aSource.Play();
		}


	}

	

}
