using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour {
	float pause = 1f;
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
