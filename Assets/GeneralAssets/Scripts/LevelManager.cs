using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour { 

	public Scene CurrentScene { get { return SceneManager.GetActiveScene();} }

	void Start() {
		DontDestroyOnLoad(gameObject);
	}

	public void LoadLevel (string name) {
		SceneManager.LoadScene(name);
	}

	public void QuitRequest() {
		Debug.Log("Recieved quit request");
		Application.Quit();
	}

	public void LoadNextLevel () {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 

	}

	public void LoadGame(Game game) {
		
		GameManager manager = FindObjectOfType<GameManager>();
		manager.CurrentGame = game; 
		SceneManager.LoadScene("Game");

	}

	public void StartNewGame() {
		
		GameManager manager = FindObjectOfType<GameManager>();
		manager.CurrentGame = null;
		SceneManager.LoadScene("Game");

	}
}
