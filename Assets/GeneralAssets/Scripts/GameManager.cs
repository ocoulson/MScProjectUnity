using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public CheckPointList CheckPoints {get {return currentGame.CheckPoints;} }

	private PlayerAdapter playerAdapter;

	private List<NpcAdapter> NPCs;

	private static Game currentGame;

	public Game CurrentGame {
		get {
			return currentGame;
		}
		set {
			currentGame = value;
		}
	}

	private bool GameStarted = false;
	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
	}	

	public void StartGame ()
	{
		
		if (currentGame == null) {
			//TODO: Call this method from main menu - give player options to set parameters.
			currentGame = NewGame ("Eve", Gender.FEMALE, "Eve2");
			CheckPoints.Add ("SpokenToMayorFirst");
			CheckPoints.Add ("SpokenToEthan");
			CheckPoints.Add ("FirstEthanMeetingPositive");
			CheckPoints.Add ("MayorLeaveBeach");
			CheckPoints.Add ("BeachRecyclePointFull");
			CheckPoints.Add("StartSortingMiniGame");

		}

		InstantiatePlayer (currentGame);

		if (NPCs == null) {
			NPCs = new List<NpcAdapter> ();
		}
		foreach (Npc npc in currentGame.CurrentNpcs) {
			InstantiateNpc (npc);
		}
		foreach (RecyclePoint point in currentGame.RecyclePoints) {
			InstantiateRecyclePoint(point);
		}
		if (currentGame.Player.InventoryInitialised) {
			foreach (InventoryItem item in currentGame.Player.Inventory.Items) {
				item.InitialiseItem();
			}
		}


		GameStarted = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (!GameStarted) return;

		if (CheckPoints ["FirstEthanMeetingPositive"] == CP_STATUS.TRIGGERED) {
			FirstEthanMeetingPositive ();
			CheckPoints ["FirstEthanMeetingPositive"] = CP_STATUS.FINISHED;
		}

		if (CheckPoints ["SpokenToEthan"] == CP_STATUS.TRIGGERED) {
			SpokenToEthan ();
			CheckPoints ["SpokenToEthan"] = CP_STATUS.FINISHED;
		}

		if (CheckPoints ["MayorLeaveBeach"] == CP_STATUS.TRIGGERED) {
			Debug.Log ("MayorLeaveBeach - not implemented");
		}
		if (CheckPoints ["BeachRecyclePointFull"] == CP_STATUS.TRIGGERED) {
			NpcAdapter ethan = FindNPC("Ethan");
			ethan.GetComponent<NpcDialogue>().SetCurrentDialogueBlock(5);
			CheckPoints["BeachRecyclePointFull"] = CP_STATUS.FINISHED;
		}
	}

	//Method to be re written when serialisation/deserialisation implemented.
	private Game NewGame(string playerName, Gender gender, string spriteName) {

		Game game = new Game(new Player(playerName, gender, spriteName, GameObject.Find("StartGamePosition").transform.position));

		GameObject mayorSpawn = GameObject.Find("MayorSpawnLocation-Beach");
		GameObject ethanSpawn = GameObject.Find("EthanSpawnLocation-Hut");

		Npc mayor = new Npc("Mayor", "Mayor", mayorSpawn.transform.position, 1f);
		Npc ethan = new Npc("Ethan", "Ethan", ethanSpawn.transform.position, 0.2f);

		RecyclePoint beachPoint = new RecyclePoint("BeachRecyclePoint", 50);
		game.RecyclePoints.Add(beachPoint);

		game.AddNpc(mayor);
		game.AddNpc(ethan);

		return game;
	}

	public void SaveGame ()
	{
		Debug.Log("Save game: " + CurrentGame.ToString());
		SaveLoad.Save(CurrentGame);
	}

	//TODO: Method to be re written when serialisation/deserialisation implemented.
	private void InstantiatePlayer (Game game)
	{
		GameObject player = Instantiate (Resources.Load ("Prefabs/Player"), game.Player.CurrentPosition, Quaternion.identity) as GameObject;
		player.name = "Player";
		playerAdapter = player.GetComponent<PlayerAdapter>();

		playerAdapter.Player = game.Player;
		game.Player.AddObserver(playerAdapter);
	}

	//TODO: Method to be re written when serialisation/deserialisation implemented.
	private void InstantiateNpc (Npc newNpc)
	{
		GameObject npcGameObject = Instantiate(Resources.Load("Prefabs/NPC"),newNpc.CurrentStartPosition, Quaternion.identity) as GameObject;
		npcGameObject.name = newNpc.Name;

		NpcAdapter adapter = npcGameObject.GetComponent<NpcAdapter>();
		adapter.Npc = newNpc;
		NPCs.Add(adapter);
		GameObject holder = GameObject.Find("NPCs");
		npcGameObject.transform.parent = holder.transform;
	}

	private void InstantiateRecyclePoint (RecyclePoint point)
	{
		RecyclePointAdapter adapter = Array.Find (GameObject.FindObjectsOfType<RecyclePointAdapter> (), RPAdapter => RPAdapter.RecyclePointName == point.Name);

		if (adapter != null) {
			adapter.RecyclePoint = point;

			RecyclePointUI uiObserver = GameObject.FindObjectOfType<RecyclePointUI> ();
			point.AddObserver (uiObserver);
		} else {
			Debug.LogError("RecyclePointAdapter with name: " + point.Name + " not found");
		}
	}




	public void AddNPC (NpcAdapter npc)
	{
		NPCs.Add(npc);
	}

	private NpcAdapter FindNPC (string name)
	{
		NpcAdapter target = Array.Find (NPCs.ToArray (), npc => npc.NpcName == name);
		if (target == null) {
			throw new UnityException ("NPC not found in list of NPCs");
		} else {
			return target;
		}
	}

	private void FirstEthanMeetingPositive ()
	{
		CheckPoints ["SpokenToEthan"] = CP_STATUS.TRIGGERED;
		playerAdapter.AddTool(new Grabber());
		GameObject backpack = Instantiate (Resources.Load ("Prefabs/Wearables/Backpack")) as GameObject;
		playerAdapter.SetWearable (backpack);
		playerAdapter.InitialiseInventory(20);
	}

	private void SpokenToEthan() {
		NpcAdapter mayor = FindNPC("Mayor");
		
		if (CheckPoints ["SpokenToMayorFirst"] == CP_STATUS.TRIGGERED) {
			mayor.gameObject.GetComponent<NpcDialogue> ().SetCurrentDialogueBlock (2);
		} else {
			mayor.gameObject.GetComponent<NpcDialogue> ().SetCurrentDialogueBlock (3);
		}
	}
}

public enum CP_STATUS {UNTRIGGERED, TRIGGERED, FINISHED}