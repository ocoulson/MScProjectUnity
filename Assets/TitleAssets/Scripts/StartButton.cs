using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]

public class StartButton : MonoBehaviour {
	public AudioClip sound;

	private AudioSource source { get { return GetComponent<AudioSource> (); } }
	private Button button { get { return GetComponent<Button> (); } }
	private bool loadingInitiated = false;
	private LevelManager levelManager;


	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();

		gameObject.AddComponent<AudioSource>(); 
		source.clip = sound;
		source.playOnAwake = false;

		button.onClick.AddListener(() => ButtonAction());
	}

	void ButtonAction ()
	{
		if (!loadingInitiated) {
			StartCoroutine(DelayedLoad());
			loadingInitiated = true;
		}

	}

	IEnumerator DelayedLoad ()
	{
		source.PlayOneShot(sound);
		yield return new WaitForSeconds(sound.length);
		levelManager.LoadNextLevel();

	}
	

}
