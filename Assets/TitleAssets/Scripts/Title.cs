using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
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
			
		aSource.clip = sounds [1];
		aSource.Play();

	}
}
