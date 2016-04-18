using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour {
	float timer = 0f;
	public GameObject canvas;
	public GameObject[] text;
	bool tDone = false;
	bool ngDone = false;
	bool cDone = false;
	bool oDone = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (timer >= 1f && !tDone) {
			SpawnText(0);
			tDone = true;
		} else if (timer >= 2f && !ngDone) {
			SpawnText(1);
			ngDone = true;
		} else if (timer >= 3f && !cDone) {
			SpawnText(2);
			cDone = true;
		} else if (timer >= 4f && !oDone) {
			SpawnText(3);
			oDone = true;
		}
	}

	void SpawnText (int i) {
		GameObject newText = GameObject.Instantiate (text [i], text [i].transform.position, Quaternion.identity) as GameObject;
		newText.transform.SetParent(canvas.transform, false);
	}
}
