using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerView : MonoBehaviour {

	private PlayerModel player;
	public PlayerModel Player { get {return player;} set {player = value; } }

	public GameObject toolSlot;
	public GameObject wearableSlot;

	private GameObject wearable;
	public GameObject Wearable { get {return wearable;} private set {wearable = value; } }

	public GameObject currentTool;
	public GameObject CurrentTool { get {return currentTool;} private set {currentTool = value; } }

	private List<GameObject> tools;
	public List<GameObject> Tools { get {return tools;} set {tools = value;} }

	private SpriteRenderer spriteRenderer;
	private ToolDisplayManager toolDisplay;
	private InventoryUIManager inventoryUiManager;
	private ThoughtBubbleManager thoughtBubbleManager;

	private Sprite[] sprites;

	public bool InventoryInitialised { get { return player.InventoryInitialised;} }
	private bool ToolEquipped { get { return currentTool != null; } }
	public bool IsThoughtBubbleActive {get { return thoughtBubbleManager.IsThoughtBubbleActive (); }}
	public  GameObject currentArea {get; set;}

	void Start ()
	{
		if (tools == null) {
			tools = new List<GameObject> ();
		}
		toolDisplay = FindObjectOfType<ToolDisplayManager>();
		inventoryUiManager = FindObjectOfType<InventoryUIManager>();
		thoughtBubbleManager = FindObjectOfType<ThoughtBubbleManager>();

		sprites = Resources.LoadAll<Sprite> ("Player/" + player.SpriteName);
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprites[4];
	}

	void Update ()
	{
		if (ToolEquipped && Input.GetKeyDown(KeyCode.E)) {
			Tool tool = CurrentTool.GetComponent<Tool> ();
			InventoryItem pickedUp = null;

			try {
				pickedUp = tool.Use ();
			} catch (UnityException ex) {
//				Debug.Log (ex.Message);
			}

			if (pickedUp != null) {

				try {
					PutItemInInventory (pickedUp);
				} catch (UnityException ex) {
					Debug.Log (ex.Message);
					StartCoroutine (InventoryFullEvent (pickedUp));
				}

			}

		}
		if (InventoryInitialised && Input.GetKeyDown (KeyCode.I)) {
			inventoryUiManager.Toggle ();
		}
		if (InventoryInitialised && Input.GetKey(KeyCode.RightShift) && Input.GetKeyUp(KeyCode.O)) {
			DropAllItems();
		}

	}

	//Player Sprite depends on the spriteName field of the PlayerModel
	void LateUpdate() {
		string spriteName = spriteRenderer.sprite.name;
		Sprite newSprite = Array.Find(sprites, sprite => sprite.name == spriteName);

		if (newSprite) {
			spriteRenderer.sprite = newSprite;
		} 
	}

	IEnumerator InventoryFullEvent(InventoryItem item) {
		GetComponent<PlayerMovement>().DisableMovement();
		thoughtBubbleManager.ShowThoughtBubble("My backpack is full, I can't carry this");
		yield return new WaitForSeconds(1f);

		thoughtBubbleManager.HideThoughtBubble();
		DropItem(item);
		GetComponent<PlayerMovement>().EnableMovement();
	}

	public List<InventoryItem> DepositEntireInventory ()
	{
		List<InventoryItem> output = player.Inventory.RemoveAll();
		return output;
	}

	//TODO: Rewrite - use new MVC pattern.
//	public void AddMultipleItems (List<InventoryItem> input)
//	{
//		foreach (InventoryItem item in input) {
//			try {
//				inventory.AddItem (item);
//			} catch (UnityException ex) {
//				Debug.Log (ex.Message);
//				StartCoroutine (InventoryFullEvent (item));
//			}
//		}
//	}

	public void DropItem(InventoryItem item) {
		player.Inventory.RemoveItem(item);

		GameObject rubbishItem = GameObject.FindObjectOfType<ItemDatabase>().CreateItemGameObject(item);
		rubbishItem.transform.parent = currentArea.GetComponentInChildren<RubbishSpawner>().transform;
		float dropRadius = 0.2f;
		rubbishItem.transform.position = gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-dropRadius,dropRadius),
										 UnityEngine.Random.Range(-dropRadius,dropRadius));
	}

	public void DropAllItems ()
	{
		InventoryItem[] items = Player.Inventory.Items.ToArray();
		foreach (InventoryItem item in items) {
			DropItem(item);
		}
	}

	public void InitialiseInventory (int initialSize)
	{
		player.InitialiseInventory(initialSize);

		inventoryUiManager.LinkInventoryToUI(player.Inventory);
	}

	public void IncreaseInventorySize (int newSize)
	{
		player.IncreaseInventorySize(newSize);
	}

	public void PutItemInInventory (InventoryItem item)
	{
		player.AddItem(item);
	}


	//TODO: Implement in PlayerModel
	public void SetWearable (GameObject newWearable)
	{
		wearable = newWearable;
		SetParentAndPosition(wearable.transform, wearableSlot.transform);
	}

	//TODO: Fix to work in PlayerModel
	public void AddTool (GameObject tool)
	{
		if (!tools.Contains (tool)) {
			tools.Add (tool);
			if (currentTool == null) {
				SetCurrentTool (tools.IndexOf (tool));
			}
		}
	}

	//TODO: Fix to work in PlayerModel
	public void SetCurrentTool (int index)
	{
		if (index > tools.Count - 1) {
			Debug.LogError ("Invalid tool choice");
		} else {
			currentTool = tools [index];
			toolSlot.transform.DetachChildren ();
			SetParentAndPosition (currentTool.transform, toolSlot.transform);

			toolDisplay.ShowToolImage ();
			toolDisplay.SetToolImage (currentTool.GetComponent<Grabber> ().icon);
		}
	}

	private void SetParentAndPosition(Transform target, Transform holder) {
		target.parent = holder;
		target.position = holder.position;
		target.rotation = holder.rotation;
	}

	public void DisplayThoughtBubble (int thoughtDialogueId)
	{

		if (player.Thoughts == null) {
			ReadJSON reader = FindObjectOfType<ReadJSON> ();
			player.Thoughts = reader.GetCharacterDialogue ("playerThoughtBubbles");
		}

		string text = "";

		foreach (DialogueBlock dBlock in player.Thoughts) {
			if (dBlock.id == thoughtDialogueId) {
				text = dBlock.script_en_GB[0];
			}
		}
		thoughtBubbleManager.ShowThoughtBubble(text);
	}

	public void HideThoughtBubble() {
		Debug.Log("Hide Thought Bubble called");
		thoughtBubbleManager.HideThoughtBubble();
	}





}
