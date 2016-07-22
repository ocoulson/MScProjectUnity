using UnityEngine;
using System.Collections;
using DialogueBlocks;

public class NPCDialogue : MonoBehaviour {

	private NPC npc;
	private NpcController npcController;
	private NpcInteractionZone zone;
	private GameObject player;

	private DialogueBlock[] dialogue;
	private DialogueBlock currentDialogueBlock;
	private int currentLine;
	private bool blockFinished;
	private bool currentIsBranch;
	private bool currentHasEffect;

	private DialogueManager dManager;
	private InstructionManager iManager;
	// Use this for initialization
	void Start () {
		npc = GetComponent<NPC>();
		npcController = GetComponent<NpcController>();
		zone = GetComponentInChildren<NpcInteractionZone>();
		dManager = FindObjectOfType<DialogueManager>();
		iManager = FindObjectOfType<InstructionManager>();
		player = FindObjectOfType<Player>().gameObject;
	}



	// Update is called once per frame
	void Update ()
	{
		if (zone.playerInZone) {
			npcController.TurnTowards (player.transform.position);
			npcController.movementEnabled = false;

			if (dialogue == null) {
				ReadDialogueData ();
			}
			if (!dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Space)) {
				iManager.HideInstruction ();
				player.GetComponent<PlayerMovement> ().movementEnabled = false;


				dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);

				IncrementDialogueCounter ();


			} else if (dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Space)) {
				dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);

				IncrementDialogueCounter ();


			} else if (blockFinished && currentIsBranch) {
				if (Input.GetKeyDown (KeyCode.Y)) {
					SetCurrentDialogueBlock ((currentDialogueBlock as BranchDialogueBlock).yesNextId);


				} else if (Input.GetKeyDown (KeyCode.N)) {
					SetCurrentDialogueBlock ((currentDialogueBlock as BranchDialogueBlock).noNextId);

				}
				dManager.ShowDialogueBox (currentDialogueBlock.script_en_GB [currentLine]);
				IncrementDialogueCounter ();

			} else if (blockFinished && !currentIsBranch && Input.GetKeyDown (KeyCode.C)) {
				CloseDialogue (true);

			} else if (dManager.DialogueActive () && Input.GetKeyDown (KeyCode.Escape)) {
				CloseDialogue (false);
			}
		} else {
			npcController.movementEnabled = true;
		}

	
	}

	void ReadDialogueData ()
	{
		ReadJSON jsonReader = GameObject.FindObjectOfType<ReadJSON> ();
		dialogue = jsonReader.GetCharacterDialogue (npc.npcName);

		SetCurrentDialogueBlock(0);
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
			if (currentDialogueBlock is LinearEffectDialogueBlock) {
				currentHasEffect = true;
			} else {
				currentHasEffect = false;
			}
		}
		currentLine = 0;
		blockFinished = false;
	}

	private void CloseDialogue (bool finished)
	{
		dManager.HideDialogueBox ();
		iManager.ShowInstruction ("Press", "Space", "to Talk");
		EnableCharacterMovement ();

		currentLine = 0;

		if (finished) {
			SetCurrentDialogueBlock((currentDialogueBlock as LinearDialogueBlock).nextId);
		}
	}

	/**
	* Simple method to freeze/unfreeze the player and npc so no movement is allowed during the dialogue
	*/ 
	private void EnableCharacterMovement ()
	{
		player.GetComponent<PlayerMovement>().movementEnabled = true;
		gameObject.GetComponent<NpcController>().movementEnabled = true;

	}

	/**
	* Handles the dialogue counter variable and displays an appropriate instruction at the base of the dialogue
	* box. 
	*/
	private void IncrementDialogueCounter ()
	{
		dManager.HideAllInstructions ();
		if (currentLine < currentDialogueBlock.script_en_GB.Length - 1) {
			currentLine++;

			dManager.ShowRightInstruction ("Press Space to Continue");
		} else {
			blockFinished = true;
			if (!currentIsBranch) {
				dManager.ShowRightInstruction ("Press C to Close");
			} else {
				dManager.ShowLeftInstruction ("Press Y for Yes");
				dManager.ShowRightInstruction ("Press N for No");
			}

			if (currentHasEffect) {
				string effectName = (currentDialogueBlock as LinearEffectDialogueBlock).effectName;

				FindObjectOfType<GameProgress>().checkPoints[effectName] = true;
			}

		}
	}
}
