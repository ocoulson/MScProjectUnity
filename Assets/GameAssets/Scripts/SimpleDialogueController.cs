using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DialogueBlocks;

public class SimpleDialogueController : MonoBehaviour {

	
	public string characterName;
	public GameObject player;

	private DialogueBlock[] dialogue;
	private DialogueBlock currentDialogueBlock;
	private int currentLine;
	private bool blockFinished;

	private bool currentIsBranch;

	private NpcController movementController;
	private NpcInteractionZone zone;
	private DialogueManager dManager;
	private InstructionManager iManager;


	//Initialise variables and find objects needed.
	void Start ()
	{
		ReadJSON jsonReader = GameObject.FindObjectOfType<ReadJSON> ();

		dialogue = jsonReader.GetCharacterDialogue (characterName);
		foreach (DialogueBlock d in dialogue) {
			Debug.Log (d);
			foreach (string s in d.script_en_GB) {
				Debug.Log(s);
			}
		}

		SetCurrentDialogueBlock(0);

		movementController = gameObject.GetComponent<NpcController>();
		dManager = GameObject.FindObjectOfType<DialogueManager>();
		iManager = GameObject.FindObjectOfType<InstructionManager>();
		zone = transform.GetComponentInChildren<NpcInteractionZone>();
	}


	/** 
	* Simple checks using the InteractionZone element to check if the player is near the npc.
	* An array of dialogue strings is iterated through until it is finished, or until escape
	* is pressed.
	* If the dialogue is read all the way through, instead of reading it all again on a second
	* interaction, the player reads a shortened version held in the returnText array.
	*/

	void Update ()
	{
		if (zone.playerInZone) {
			
			if (!dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Space)) {
				iManager.HideInstruction ();
				movementController.TurnTowards (player.transform.position);
				EnableCharacterMovement (false);

				dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);
				dManager.HideAllInstructions ();
				IncrementDialogueCounter ();


			} else if (dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Space)) {
				dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);

				IncrementDialogueCounter ();


			} else if (blockFinished && currentIsBranch) {

				if (Input.GetKeyDown(KeyCode.Y)) {
					SetCurrentDialogueBlock((currentDialogueBlock as BranchDialogueBlock).yesNext);
				} if (Input.GetKeyDown(KeyCode.N)) {
					SetCurrentDialogueBlock((currentDialogueBlock as BranchDialogueBlock).noNext);
				}


			} else if (blockFinished && !currentIsBranch && Input.GetKeyDown (KeyCode.C)) {
				CloseDialogue(true);

			} else if (dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Escape)) {
				CloseDialogue(false);
			}
		}
		else if (dManager.DialogueActive ()) {
			dManager.HideDialogueBox();
		}
	}

	private void SetCurrentDialogueBlock (int id)
	{
		foreach (DialogueBlock block in dialogue) {
			if (block.id == id) {
				currentDialogueBlock = block;
			}
		}

		if (currentDialogueBlock is BranchDialogueBlock) {
			currentIsBranch = true;
		} else {
			currentIsBranch = false;
		}

		currentLine = 0;
		blockFinished = false;
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

		currentLine = 0;

		if (finished) {
			//TODO add code here
		}
	}

	/**
	* Simple method to freeze/unfreeze the player and npc so no movement is allowed during the dialogue
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
		if (currentLine < currentDialogueBlock.script_en_GB.Length - 1) {
			currentLine++;
			dManager.ShowRightInstruction ("Press Space to Continue");
		} else {
			blockFinished = true;
			if (!currentIsBranch) {
				dManager.ShowRightInstruction ("Press C to Close");
			} else {
				dManager.ShowLeftInstruction("Press Y for Yes");
				dManager.ShowRightInstruction("Press N for No");
			}


		}
	}
}
