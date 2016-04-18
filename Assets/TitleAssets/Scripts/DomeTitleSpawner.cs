using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DomeTitleSpawner : MonoBehaviour {
	float timer= 0f;
	bool finished = false;
	public GameObject dome;
	public GameObject title;

	public GameObject canvas;
	public GameObject startText;

	bool textSpawned = false;
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (!finished && timer >= 5f) {
			Vector3 domePos = new Vector3 (20, 35, transform.position.z);
			Vector3 titlePos = new Vector3 (20, 50, transform.position.z);
			GameObject myDome = Instantiate (dome, domePos, Quaternion.identity) as GameObject;
			GameObject myTitle = Instantiate (title, titlePos, Quaternion.identity) as GameObject;

			myDome.transform.parent = this.transform;
			myTitle.transform.parent = this.transform;
			finished = true;
		} else {
			if (timer >= 10f && !textSpawned) {
				SpawnStartText();
			}

		}
	}


	void SpawnStartText() {
		Vector3 textPos = new Vector3(0, -140, 0);

		GameObject myStartText = Instantiate(startText, textPos, Quaternion.identity) as GameObject;

		myStartText.transform.SetParent(canvas.transform,false);

		textSpawned = true;
	}



}
