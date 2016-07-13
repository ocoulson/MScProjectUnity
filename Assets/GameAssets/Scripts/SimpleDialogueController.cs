using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleDialogueController : MonoBehaviour {

	public string[] mainDialogueText;
	public string[] returnText;
	public GameObject player;

	private NpcController movementController;
	private NpcInteractionZone zone;
	private DialogueManager dManager;
	private InstructionManager iManager;

	private int dialogueCounter;
	private bool dialogueFinished;

	//Initialise variables and find objects needed.
	void Start ()
	{
		dialogueCounter = 0;
		dialogueFinished = false;
		movementController = gameObject.GetComponent<NpcController>();
		dManager = GameObject.FindObjectOfType<DialogueManager>();
		iManager = GameObject.FindObjectOfType<InstructionManager>();
		zone = transform.GetComponentInChildren<NpcInteractionZone>();
	}



	void Update ()
	{
		SimpleDialogueLoop();
	}
	/** 
	* Simple checks using the InteractionZone element to check if the player is near the npc.
	* An array of dialogue strings is iterated through until it is finished, or until escape
	* is pressed.
	* If the dialogue is read all the way through, instead of reading it all again on a second
	* interaction, the player reads a shortened version held in the returnText array.
	*/
	private void SimpleDialogueLoop ()
	{
		if (zone.playerInZone) {

			if (!dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Space)) {
				iManager.HideInstruction ();
				movementController.TurnTowards (player.transform.position);
				EnableCharacterMovement (false);

				dManager.ShowDialogueBox (mainDialogueText [dialogueCounter]);
				dManager.HideAllInstructions ();
				IncrementDialogueCounter ();

			} else if (dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Space)) {
				dManager.ShowDialogueBox (mainDialogueText [dialogueCounter]);

				IncrementDialogueCounter ();

			} else if (dialogueFinished && Input.GetKeyDown (KeyCode.C)) {
				CloseDialogue(true);

			} else if (dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Escape)) {
				CloseDialogue(false);
			}
		}
		else if (dManager.DialogueActive ()) {
			dManager.HideDialogueBox();
		}
	}

	/**
	* Finishes the dialogue, either prematurely in which case the next interaction will restart
	* the whole dialogue, or if it was previously read through, it will set the dialogue to be a condensed
	* version held in the returnText array.
	*/

	private void CloseDialogue (bool finished)
	{
		dManager.HideDialogueBox ();
		iManager.ShowInstruction ("Press", "Space", "to Talk");
		EnableCharacterMovement (true);

		dialogueCounter = 0;

		if (finished) {
			mainDialogueText = returnText;
		}
	}

	/**
	* Simple method to freeze the player and npc so no movement is allowed during the dialogue
	*/ 
	private void EnableCharacterMovement (bool toggle)
	{
		player.GetComponent<PlayerMovement>().movementEnabled = toggle;
		movementController.movementEnabled = toggle;

	}

	/**
	* Handles the dialogue counter variable and displays an appropriate instruction at the base of the dialogue
	* box. 
	*/
	private void IncrementDialogueCounter ()
	{
		if (dialogueCounter < mainDialogueText.Length - 1) {
			dialogueCounter++;
			dManager.ShowRightInstruction("Press Space to Continue");
		} else {
			dialogueFinished = true;
			dManager.ShowRightInstruction("Press C to Close");
		}
	}
}
