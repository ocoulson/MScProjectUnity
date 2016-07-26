﻿using UnityEngine;
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
	private string dialogueJsonString;

	private JsonData itemsJsonData;
	private string itemsJsonString;


	void Start ()
	{
		DontDestroyOnLoad(gameObject);
		dialogueJsonString = File.ReadAllText (Application.dataPath + "/Resources/JSON/Dialogue.json");
		dialogueJsonData = JsonMapper.ToObject (dialogueJsonString);

		itemsJsonString = File.ReadAllText(Application.dataPath + "/Resources/JSON/InventoryItems.json");

		itemsJsonData = JsonMapper.ToObject (itemsJsonString);

	}

	public Resource[] GetResourceList ()
	{
		JsonData resources =  itemsJsonData["Resources"];
		List<Resource> output = new List<Resource> ();

		for (int i = 0; i < resources.Count; i++) {
			output.Add(JsonMapper.ToObject<Resource>(resources[i].ToJson()));
		}
		return output.ToArray();
	}

	public Rubbish[] GetRubbishList ()
	{
		JsonData rubbish = itemsJsonData["Rubbish"];
		List<Rubbish> output = new List<Rubbish>();

		for (int i = 0; i < rubbish.Count; i++) {
			output.Add(JsonMapper.ToObject<Rubbish>(rubbish[i].ToJson()));
		}
		return output.ToArray(); 
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





}

