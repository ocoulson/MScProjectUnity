using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour {

	public GameObject pauseMenuPanel;
	 
	
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
		PlayerMovement movement = GameObject.Find("Player").GetComponent<PlayerMovement>();

		if (active == true) {
			Time.timeScale = 1.0f;
			movement.EnableMovement();
		} else {
			Time.timeScale = 0f;
			movement.DisableMovement();
		}

		pauseMenuPanel.SetActive(!active);
	}
}
