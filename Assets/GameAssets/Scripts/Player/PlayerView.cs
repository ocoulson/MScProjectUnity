using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using ObserverPattern;

public class PlayerView : MonoBehaviour, Observer  {

	private PlayerModel player;
	public PlayerModel Player { get {return player;} set {player = value; } }

	public GameObject toolSlot;
	public GameObject wearableSlot;

	private GameObject wearable;
	public GameObject Wearable { get {return wearable;} private set {wearable = value; } }

	private GameObject currentToolObject;
	public GameObject CurrentToolObject { get {return currentToolObject;} private set {currentToolObject = value; } }

	private SpriteRenderer spriteRenderer;
	private ToolDisplayManager toolDisplay;
	private InventoryUIManager inventoryUiManager;
	private ThoughtBubbleManager thoughtBubbleManager;

	private Sprite[] sprites;

	public bool InventoryInitialised { get { return player.InventoryInitialised;} }
	private bool ToolEquipped { get { return player.CurrentTool != null; } }
	public bool IsThoughtBubbleActive {get { return thoughtBubbleManager.IsThoughtBubbleActive (); }}
	public  GameObject currentArea {get; set;}

	void Start ()
	{

		toolDisplay = FindObjectOfType<ToolDisplayManager> ();
		inventoryUiManager = FindObjectOfType<InventoryUIManager> ();
		thoughtBubbleManager = FindObjectOfType<ThoughtBubbleManager> ();

		sprites = Resources.LoadAll<Sprite> ("Player/" + player.SpriteName);
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprites [4];

		if (player.CurrentTool != null) {
			UpdateCurrentTool();
		}
	}

	void Update ()
	{
		if (ToolEquipped && Input.GetKeyDown(KeyCode.E)) {
			ToolAdapter tool = CurrentToolObject.GetComponent<ToolAdapter> ();
			InventoryItem pickedUp = null;

			try {
				pickedUp = tool.Use ();
			} catch (UnityException ex) {
				Debug.Log (ex.Message);
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

	#region Observer implementation

	public void OnNotify ()
	{
		
		if (CurrentToolObject == null || CurrentToolObject.GetComponent<ToolAdapter> ().Tool != Player.CurrentTool) {
			UpdateCurrentTool();
		}
	}

	#endregion

	IEnumerator InventoryFullEvent (InventoryItem item)
	{
		GetComponent<PlayerMovement> ().DisableMovement ();

		string thoughtText = "My backpack is full, I can't carry this";
		thoughtBubbleManager.ShowThoughtBubble(thoughtText);

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

	public void AddTool(Tool tool) {
		player.AddTool(tool);
		Debug.Assert(player.CurrentTool == tool);
	}

	public void SetCurrentTool (int index)
	{
		Player.SetCurrentTool(index);
	}

	//Creates a new gameObject to hold the Tool currentTool in the playerModel and attaches it to the 
	//Player game object.
	private void UpdateCurrentTool ()
	{
		GameObject newToolObject = GameObject.Instantiate(Resources.Load("Prefabs/Tools/Tool")) as GameObject;
		ToolAdapter toolAdapter = newToolObject.GetComponent<ToolAdapter>();
		toolAdapter.Tool = Player.CurrentTool;
		toolAdapter.InitialiseSprite();

		CurrentToolObject = newToolObject;

		toolSlot.transform.DetachChildren();
		SetParentAndPosition(newToolObject.transform, toolSlot.transform);

		toolDisplay.ShowToolImage ();
		toolDisplay.SetToolImage (Player.CurrentTool.Icon);
	}

	private void SetParentAndPosition(Transform target, Transform holder) {
		target.parent = holder;
		target.position = holder.position;
		target.rotation = holder.rotation;
	}

	public void DisplayThoughtBubble (int thoughtDialogueId)
	{
		if (player.Thoughts == null) {
			InstantiateThoughts();
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

	public void UpdatePlayerPosition(Vector2 newPosition) {
		player.CurrentPosition = newPosition;
	}

	private void InstantiateThoughts() {
		
			ReadJSON reader = FindObjectOfType<ReadJSON> ();
			player.Thoughts = reader.GetCharacterDialogue ("playerThoughtBubbles");
		

	}
}
