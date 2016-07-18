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
	private bool playerInZone;

	private NpcController movementController;
	private DialogueManager dManager;
	private InstructionManager iManager;


	//Initialise variables and find objects needed.
	void Start ()
	{
		movementController = gameObject.GetComponent<NpcController>();
		dManager = GameObject.FindObjectOfType<DialogueManager>();
		iManager = GameObject.FindObjectOfType<InstructionManager>();

	}

	void ReadDialogueData ()
	{
		ReadJSON jsonReader = GameObject.FindObjectOfType<ReadJSON> ();
		dialogue = jsonReader.GetCharacterDialogue (characterName);

		SetCurrentDialogueBlock(0);
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
		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKeyDown (KeyCode.Keypad0)) {
			Debug.Log(currentDialogueBlock);
		}
		if (playerInZone) {

			if (!dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Space)) {
				if (dialogue == null) {
					ReadDialogueData();
				}
				iManager.HideInstruction ();
				player.GetComponent<PlayerMovement>().movementEnabled = false;


				dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);

				IncrementDialogueCounter ();


			} else if (dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Space)) {
				dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);

				IncrementDialogueCounter ();


			} else if (blockFinished && currentIsBranch) {
				if (Input.GetKeyDown(KeyCode.Y)) {
					SetCurrentDialogueBlock((currentDialogueBlock as BranchDialogueBlock).yesNextId);

				} else if (Input.GetKeyDown(KeyCode.N)) {
					SetCurrentDialogueBlock((currentDialogueBlock as BranchDialogueBlock).noNextId);

				}
				dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);
				IncrementDialogueCounter();

			} else if (blockFinished && !currentIsBranch && Input.GetKeyDown (KeyCode.C)) {
				CloseDialogue(true);

			} else if (dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Escape)) {
				CloseDialogue(false);
			}
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
	* For linear dialogue blocks with no branches
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
			SetCurrentDialogueBlock((currentDialogueBlock as LinearDialogueBlock).nextId);
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
		dManager.HideAllInstructions();
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

	public void PlayerEnterInteractionZone() {
		Debug.Log("Player entered " + gameObject.name + "'s zone");
		playerInZone = true;
		movementController.TurnTowards (player.transform.position);
		movementController.movementEnabled = false;
	}

	public void PlayerExitInteractionZone() {
		Debug.Log("Player left " + gameObject.name + "'s zone");
		playerInZone = false;
		movementController.movementEnabled = true;
	}
}
