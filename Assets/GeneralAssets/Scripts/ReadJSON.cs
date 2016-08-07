using UnityEngine;
using LitJson;
using DialogueBlocks;


/**
*	Behaviour to hold the JsonDataHandler object and provide access to its methods from the scene.
*/
public class ReadJSON : MonoBehaviour {

	private JsonDataHandler handler;

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

