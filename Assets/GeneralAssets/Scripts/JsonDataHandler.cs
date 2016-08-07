using LitJson;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using DialogueBlocks;

public class JsonDataHandler  {

	//Might want to make this a singleton 


	private JsonData dialogueJsonData;
	private JsonData itemsJsonData;

	//Properties to encapsulate the JsonData objects for testing
	public JsonData DialogueJsonData {
		get { return dialogueJsonData; }
	}
	public JsonData ItemsJsonData {
		get { return itemsJsonData; }
	}

	public JsonDataHandler() {

		dialogueJsonData = GetJsonDataObjectFromFile("/Resources/JSON/Dialogue.json");

		itemsJsonData = GetJsonDataObjectFromFile("/Resources/JSON/InventoryItems.json");
	}

	public JsonData GetJsonDataObjectFromFile (string filePath) 
	{
		string jsonString = File.ReadAllText (Application.dataPath + filePath);
		JsonData jsonData = JsonMapper.ToObject(jsonString);
		return jsonData;
	}


	public InventoryItem[] GetInventoryItemArray(string itemType) {

		JsonData items =  itemsJsonData[itemType];

		List<InventoryItem> output = new List<InventoryItem> ();

		for (int i = 0; i < items.Count; i++) {
			InventoryItem item = JsonMapper.ToObject<InventoryItem>(items[i].ToJson());
			item.InitialiseItem();
			output.Add(item);
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
