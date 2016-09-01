using UnityEngine;
using System.Collections;

public class SortingGameLauncher : MonoBehaviour {
	public GameObject startPanel;


	private DialogueUIManager dManager;
	private ReadJson reader;

	private DialogueBlock[] dialogue;
	private DialogueBlock currentDialogueBlock;
	private int currentLine = 0;

	private bool blockFinished;
	private bool justStarted;

	// Use this for initialization
	void Start () {
		
		dManager = FindObjectOfType<DialogueUIManager>();
		reader = FindObjectOfType<ReadJson>();
		dialogue = reader.GetCharacterDialogue("sortingGameIntro");
		currentDialogueBlock = dialogue[0];
		Time.timeScale = 0;
		blockFinished = false;
		justStarted = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (justStarted) {
			DisplayLine ();
			justStarted = false;
			return;
		}

		if (blockFinished) {
			if (Input.GetKeyDown (KeyCode.Return)) {
				startPanel.SetActive(false);
				Time.timeScale = 1f;
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			DisplayLine();
		}
	}
	void DisplayLine ()
	{
		dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);
		IncrementDialogueCounter ();
	}

	private void IncrementDialogueCounter ()
	{
		dManager.HideAllInstructions ();
		if (currentLine < currentDialogueBlock.script_en_GB.Length - 1) {
			currentLine++;

			dManager.ShowRightInstruction ("Press Space to Continue");
		} else {
			blockFinished = true;
			dManager.ShowRightInstruction ("Press Enter to Start");
		}
	}
}
