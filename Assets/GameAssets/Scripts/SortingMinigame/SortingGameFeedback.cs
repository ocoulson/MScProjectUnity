using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortingGameFeedback : MonoBehaviour {

	public RecyclingReceiverFeedbackUI[] feedbackItems;
	private bool partOfGame;
	
	public void DisplayFeedback (bool partOfGame)
	{
		Time.timeScale = 0;
		this.partOfGame = partOfGame;
		foreach(RecyclingReceiverFeedbackUI rrfUI in feedbackItems) {
			rrfUI.SetupUI();
			rrfUI.PopulateSlots();
		}
	}

	public void ButtonHandler() {
		LevelManager levelManager = FindObjectOfType<LevelManager> ();

			if (partOfGame) {
				levelManager.LoadGame (FindObjectOfType<GameManager>().CurrentGame);
			} else {
				levelManager.LoadLevel("MainMenu");
			}
			Time.timeScale = 1f;
	}
}
