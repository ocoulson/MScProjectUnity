using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour {
	float pause = 1f;
	public GameObject canvas;
	public GameObject[] text;

	public GameObject exitButton;

	void Start ()
	{
		StartCoroutine (SpawnText ());

	}

	IEnumerator SpawnText ()
	{
		foreach (GameObject go in text) {
			SpawnGameObject(go);
			yield return new WaitForSeconds(pause);
		}
		if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer) {
			SpawnGameObject(exitButton);

		}
	}

	private void SpawnGameObject (GameObject go)
	{
		GameObject newText = GameObject.Instantiate (go, go.transform.position, Quaternion.identity) as GameObject;
		newText.transform.SetParent(canvas.transform, false);
	}

}
