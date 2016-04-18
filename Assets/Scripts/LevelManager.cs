using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour { 
	private Scene currentScene;
	private bool paused = false;
	private bool canPause = false;

	public void LoadLevel (string name) {
		SceneManager.LoadScene(name);
		currentScene = SceneManager.GetActiveScene();
	}

	public void QuitRequest() {
		Debug.Log("Recieved quit request");
		Application.Quit();
	}

	public void LoadNextLevel () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
		currentScene = SceneManager.GetActiveScene();
	}

	void Update ()
	{
		if ((currentScene.name == "Title") || (currentScene.name == "Splash")) {
			canPause = false;
		} else {
			canPause = true;
		}
		if (paused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

		if (Input.GetKeyDown (KeyCode.Space) && canPause) {
			if (paused) {
				paused = false;
			} else {
				paused = true;
			}
		}
	}

	public bool Paused () {
		return paused;
	}
}
