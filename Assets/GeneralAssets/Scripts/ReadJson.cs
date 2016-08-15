using UnityEngine;
using LitJson;



/**
*	Behaviour to hold the JsonDataHandler object and provide access to its methods from the scene.
*/
public class ReadJson : MonoBehaviour {

	private JsonDataHandler handler;
	public JsonData DialogueJsonData {
		get { return handler.DialogueJsonData;}
	}
	public JsonData ItemsJsonData {
		get { return handler.ItemsJsonData; }
	}
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		if (handler == null) {
			handler = new JsonDataHandler();
		}

	}

	public InventoryItem[] GetResourceList ()
	{
		return handler.GetInventoryItemArray("Resources");
	}

	public InventoryItem[] GetRubbishList ()
	{
		return handler.GetInventoryItemArray("Rubbish");
	}

	public DialogueBlock[] GetCharacterDialogue (string character)
	{
		return handler.GetCharacterDialogue(character);
	}





}

