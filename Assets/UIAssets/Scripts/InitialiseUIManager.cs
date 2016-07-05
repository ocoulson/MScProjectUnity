using UnityEngine;
using System.Collections;

public class InitialiseUIManager : MonoBehaviour {
	float timer = 0f;
	public GameObject text;
	LevelManager levelManager;
	// Use this for initialization
	void Start () {
		GameObject newObject = GameObject.Instantiate(text, text.transform.position,Quaternion.identity) as GameObject;
		newObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform, false);
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

	void Update ()
	{
		timer += Time.deltaTime;
		if (timer > 2f) {
			levelManager.LoadNextLevel();
		}
	}


}
