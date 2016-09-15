using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DomeTitleSpawner : MonoBehaviour {
	bool finished = false;
	public GameObject dome;
	public GameObject title;
	public GameObject startText;
	bool textSpawned = false;
	float timer = 0;

	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (!finished && (Input.anyKeyDown|| timer > 5f ||Input.GetKeyDown(KeyCode.Space))) {
			//Create vectors for spawn points of dome and title
			Vector3 domePos = new Vector3 (20, 35, transform.position.z);
			Vector3 titlePos = new Vector3 (20, 50, transform.position.z);
			//Instantiate dome and title
			GameObject myDome = Instantiate (dome, domePos, Quaternion.identity) as GameObject;
			GameObject myTitle = Instantiate (title, titlePos, Quaternion.identity) as GameObject;
			//Set dome and title as children of the Spawner object
			myDome.transform.parent = this.transform;
			myTitle.transform.parent = this.transform;

			finished = true;

		} else if (!textSpawned && (Input.anyKeyDown|| timer > 15f)) {
			//Set vector for text button spawn point
			Vector3 textPos = new Vector3 (0, -140, 0);
			//Instantiate button
			GameObject myStartText = Instantiate (startText, textPos, Quaternion.identity) as GameObject;
			//Set the button as a child of the UI canvas object
			myStartText.transform.SetParent (GameObject.FindObjectOfType<Canvas> ().transform, false);

			textSpawned = true;
		}

	}

}
