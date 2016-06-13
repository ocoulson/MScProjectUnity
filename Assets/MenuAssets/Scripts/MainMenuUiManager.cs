using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUiManager : MonoBehaviour {
	float timer = 0f;
	public GameObject initialiseText;
	public GameObject canvas;
	public GameObject[] text;
	InitialiseType initialisedText;
	bool initialised = false;
	bool tDone = false;
	bool ngDone = false;
	bool cDone = false;
	bool oDone = false;

	void Start() {
		SpawnText(initialiseText);
		initialisedText = GameObject.FindObjectOfType<InitialiseType>();
	}
	// Update is called once per frame
	void Update ()
	{
		if (!initialised) {
			initialised = initialisedText.finished;

		}

		if (initialised) {
			
			timer += Time.deltaTime;
			if (timer >= 1f && !tDone) {
				GameObject.Destroy (initialisedText.gameObject);
				SpawnText(text[0]);
				tDone = true;
			} else if (timer >= 1.5f && !ngDone) {
				SpawnText(text[1]);
				ngDone = true;
			} else if (timer >= 2f && !cDone) {
				SpawnText(text[2]);
				cDone = true;
			} else if (timer >= 2.5f && !oDone) {
				SpawnText(text[3]);
				oDone = true;
			}
		}

	}

	void SpawnText (GameObject go) {
		GameObject newText = GameObject.Instantiate (go, go.transform.position, Quaternion.identity) as GameObject;
		newText.transform.SetParent(canvas.transform, false);

	}
}
