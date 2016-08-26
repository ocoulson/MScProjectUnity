using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

	public GameObject pauseMenuPanel;
	PlayerMovement movement;

	void Start() {
		movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			ToggleMenuVisible();

		}
	}

	void ToggleMenuVisible ()
	{
		bool active = pauseMenuPanel.activeInHierarchy;

		if (active == true) {
			Time.timeScale = 1.0f;
			movement.EnableMovement();
		} else {
			Time.timeScale = 0f;
			movement.DisableMovement();
		}

		pauseMenuPanel.SetActive(!active);
	}

	public void QuitToMainMenu() {
		ToggleMenuVisible();
		FindObjectOfType<LevelManager>().LoadLevel("MainMenu");
	}
}
