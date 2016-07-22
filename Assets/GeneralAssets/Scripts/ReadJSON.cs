using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using DialogueBlocks;


/**
*	A class to read from a JSON file and return appropriate objects.
*/
public class ReadJSON : MonoBehaviour {

	private JsonData dialogueJsonData;
	private string jsonString;

	// Use this for initialization

	void Start ()
	{
		jsonString = File.ReadAllText (Application.dataPath + "/Resources/Dialogue.json");

		dialogueJsonData = JsonMapper.ToObject (jsonString);

	}

	public DialogueBlock[] GetCharacterDialogue (string character)
	{
		JsonData actorData = dialogueJsonData ["actors"] [character];

		List<DialogueBlock> dialogueList = new List<DialogueBlock> ();

		for (int i = 0; i < actorData.Count; i++) {
			if (actorData [i] ["type"].ToString () == "linearDialogue") {
				dialogueList.Add (JsonMapper.ToObject<LinearDialogueBlock> (actorData [i].ToJson ()));
			} else if (actorData [i] ["type"].ToString () == "branchDialogue") {
				dialogueList.Add (JsonMapper.ToObject<BranchDialogueBlock> (actorData [i].ToJson ()));
			} else if (actorData [i] ["type"].ToString () == "linearEffectDialogue") {
				dialogueList.Add (JsonMapper.ToObject<LinearEffectDialogueBlock> (actorData [i].ToJson ()));
			} 

		}

		return dialogueList.ToArray();
	} 

	JsonData GetDialogueJsonData (string actor, string blockName)
	{
		JsonData actorData = dialogueJsonData ["actors"] [actor];

		for (int i = 0; i < actorData.Count; i++) {
			if (blockName == actorData [i] ["name"].ToString ()) {
				Debug.Log ("found");
				return actorData [i];
			}
		}
		Debug.Log ("not found");
		return null;
	}

	DialogueBlock GetDialogue (JsonData block)
	{
		DialogueBlock output = null;
	
		if (block ["type"].ToString () == "linearDialogue") {
			output = JsonMapper.ToObject<LinearDialogueBlock> (block.ToJson ());

		} else if (block ["type"].ToString () == "branchDialogue") {
			output = JsonMapper.ToObject<BranchDialogueBlock>(block.ToJson());
		}
			
		return output; 
	}



}

