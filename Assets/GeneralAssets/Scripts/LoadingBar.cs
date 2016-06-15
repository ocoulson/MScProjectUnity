using UnityEngine;
using System.Collections;

public class LoadingBar : MonoBehaviour {
	private LevelManager manager;

	void Start ()
	{
		manager = GameObject.FindObjectOfType<LevelManager>();
		Vector3 scale = gameObject.transform.localScale;
		scale.x = 0;
		gameObject.transform.localScale = scale;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 scale = gameObject.transform.localScale;
		if (transform.localScale.x < 10f) {
			scale.x += 0.1f;
			transform.localScale = scale;
			print(transform.localScale.x);
		} else {
			manager.LoadNextLevel();
		}
	}
}
