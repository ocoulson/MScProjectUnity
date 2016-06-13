using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUiManager : MonoBehaviour {
	float pause = 0.6f;
	public GameObject canvas;
	public GameObject[] text;



	void Start ()
	{
		StartCoroutine(SpawnText());

	}

	IEnumerator SpawnText ()
	{
		foreach (GameObject go in text) {
			
			GameObject newText = GameObject.Instantiate (go, go.transform.position, Quaternion.identity) as GameObject;
			newText.transform.SetParent(canvas.transform, false);
			yield return new WaitForSeconds(pause);
		}

	}

}
