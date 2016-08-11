using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameProgress : MonoBehaviour {
	//private Dictionary<string , CP_STATUS> checkPoints;

//	public Dictionary<string, CP_STATUS> CheckPoints {
//		get {return checkPoints;}
//		private set {checkPoints = value;}
//	}
	private CheckPointList checkPoints;

	public CheckPointList CheckPoints {
		get {
			return checkPoints;
		}
		set {
			checkPoints = value;
		}
	}

	private PlayerGameObject player;

	private List<NonPlayerCharacter> NPCs;

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
		player = FindObjectOfType<PlayerGameObject> ();

		if (NPCs == null) {
			NPCs = new List<NonPlayerCharacter> ();
		}
		foreach (NonPlayerCharacter npc in FindObjectsOfType<NonPlayerCharacter>()) {
			if (!NPCs.Contains (npc)) {
				AddNPC (npc);
			}
		}
		checkPoints = new CheckPointList();
		checkPoints.Add("SpokenToMayorFirst");
		checkPoints.Add("SpokenToEthan");
		checkPoints.Add("FirstEthanMeetingPositive");
		checkPoints.Add("MayorLeaveBeach");
		checkPoints.Add("BeachRecyclePointFull1");

		//checkPoints = new Dictionary<string,CP_STATUS>();
//		checkPoints.Add("SpokenToMayorFirst", CP_STATUS.UNTRIGGERED);
//		checkPoints.Add("SpokenToEthan", CP_STATUS.UNTRIGGERED);
//		checkPoints.Add("FirstEthanMeetingPositive", CP_STATUS.UNTRIGGERED);
//		checkPoints.Add("MayorLeaveBeach", CP_STATUS.UNTRIGGERED);
//		checkPoints.Add("BeachRecyclePointFull1", CP_STATUS.UNTRIGGERED);
	}	
	// Update is called once per frame
	void Update ()
	{
		if (checkPoints ["FirstEthanMeetingPositive"] == CP_STATUS.TRIGGERED) {
			FirstEthanMeetingPositive ();
			checkPoints ["FirstEthanMeetingPositive"] = CP_STATUS.FINISHED;
		}

		if (checkPoints ["SpokenToEthan"] == CP_STATUS.TRIGGERED) {
			SpokenToEthan ();
			checkPoints ["SpokenToEthan"] = CP_STATUS.FINISHED;
		}

		if (checkPoints ["MayorLeaveBeach"] == CP_STATUS.TRIGGERED) {
			Debug.Log ("MayorLeaveBeach - not implemented");
		}
		if (checkPoints ["BeachRecyclePointFull1"] == CP_STATUS.TRIGGERED) {
			NonPlayerCharacter ethan = FindNPC("Ethan");
			ethan.GetComponent<NPCDialogue>().SetCurrentDialogueBlock(5);
			checkPoints["BeachRecyclePointFull1"] = CP_STATUS.FINISHED;
		}
	}

	public void AddNPC (NonPlayerCharacter npc)
	{
		NPCs.Add(npc);
	}

	private NonPlayerCharacter FindNPC (string name)
	{
		NonPlayerCharacter target = Array.Find (NPCs.ToArray (), npc => npc.npcName == name);
		if (target == null) {
			throw new UnityException ("NPC not found in list of NPCs");
		} else {
			return target;
		}
	}

	private void FirstEthanMeetingPositive ()
	{
		checkPoints ["SpokenToEthan"] = CP_STATUS.TRIGGERED;
		GameObject grabber = Instantiate (Resources.Load ("Prefabs/Tools/Grabber")) as GameObject;
		player.AddTool (grabber);

		GameObject backpack = Instantiate (Resources.Load ("Prefabs/Wearables/Backpack")) as GameObject;
		player.SetWearable (backpack);
		player.InitialiseInventory(20);
	}

	private void SpokenToEthan() {
		NonPlayerCharacter mayor = FindNPC("Mayor");
		
		if (checkPoints ["SpokenToMayorFirst"] == CP_STATUS.TRIGGERED) {
			mayor.gameObject.GetComponent<NPCDialogue> ().SetCurrentDialogueBlock (2);
		} else {
			mayor.gameObject.GetComponent<NPCDialogue> ().SetCurrentDialogueBlock (3);
		}
	}
}

public enum CP_STATUS {UNTRIGGERED, TRIGGERED, FINISHED}