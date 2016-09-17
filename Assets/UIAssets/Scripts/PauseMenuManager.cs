using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

	private GameManager manager;
	public GameObject pauseMenuPanel;
	public GameObject controlsPanel;
	PlayerMovement movement;

	void Start() {
		movement = GameObject.Find("Player").GetComponent<PlayerMovement>();
		manager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			ToggleMenuVisible();

		}
	}

	public void ToggleMenuVisible ()
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

	public void SaveGame() {
		manager.SaveGame();
		ToggleMenuVisible();
	}

	public void SaveAndQuit() {
		SaveGame();
		QuitToMainMenu();
	}
	public void QuitToMainMenu ()
	{
		FindObjectOfType<LevelManager>().LoadLevel("MainMenu");
	}

	public void ShowControls ()
	{
		controlsPanel.SetActive(true);
	}
	public void HideControls ()
	{
		controlsPanel.SetActive(false);
	}


}
