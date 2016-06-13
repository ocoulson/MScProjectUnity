using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUiManager : MonoBehaviour {
	float pause = 1f;
	public GameObject canvas;
	public GameObject[] text;
	bool initialised = false;


	void Start ()
	{
		Debug.Log("1");
		StartCoroutine(SpawnText());
		//GameObject.Destroy(GameObject.FindObjectOfType<InitialiseType>().gameObject);

	}

	IEnumerator SpawnText ()
	{
		foreach (GameObject go in text) {
			if (initialised) {
				GameObject.Destroy(GameObject.FindObjectOfType<InitialiseType>().gameObject);
				initialised = false;
				pause = 0.5f;
			}
			GameObject newText = GameObject.Instantiate (go, go.transform.position, Quaternion.identity) as GameObject;
			if (go.Equals(text[0])) {
				initialised = true;
			}
			newText.transform.SetParent(canvas.transform, false);
			yield return new WaitForSeconds(pause);
		}

	}
	// Update is called once per frame
//	void Update ()
//	{
//
//
//		if (initialised) {
//			
//			timer += Time.deltaTime;
//			if (timer >= 1f && !tDone) {
//				GameObject.Destroy (initialisedText.gameObject);
//				SpawnText(text[0]);
//				tDone = true;
//			} else if (timer >= 1.5f && !ngDone) {
//				SpawnText(text[1]);
//				ngDone = true;
//			} else if (timer >= 2f && !cDone) {
//				SpawnText(text[2]);
//				cDone = true;
//			} else if (timer >= 2.5f && !oDone) {
//				SpawnText(text[3]);
//				oDone = true;
//			}
//		}
//
//	}


}
