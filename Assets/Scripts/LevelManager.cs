using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour { 
	private Scene currentScene;


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

}
