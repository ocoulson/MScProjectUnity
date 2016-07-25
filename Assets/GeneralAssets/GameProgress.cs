using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameProgress : MonoBehaviour {

	public Dictionary<string, CP_STATUS> checkPoints {get; private set;}
	private Player player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
		checkPoints = new Dictionary<string,CP_STATUS>();
		checkPoints.Add("SpokenToMayorFirst", CP_STATUS.UNTRIGGERED);
		checkPoints.Add("SpokenToEthan", CP_STATUS.UNTRIGGERED);
		checkPoints.Add("FirstEthanMeetingPositive", CP_STATUS.UNTRIGGERED);
		checkPoints.Add("MayorLeaveBeach", CP_STATUS.UNTRIGGERED);
	}	
	// Update is called once per frame
	void Update ()
	{
		if (checkPoints ["FirstEthanMeetingPositive"] == CP_STATUS.TRIGGERED) {
			checkPoints ["SpokenToEthan"] = CP_STATUS.TRIGGERED;
			GameObject grabber = Instantiate (Resources.Load ("Prefabs/Tools/Grabber")) as GameObject;
			player.AddTool (grabber);

			GameObject backpack = Instantiate (Resources.Load ("Prefabs/Wearables/Backpack")) as GameObject;
			player.SetWearable (backpack);

			checkPoints ["FirstEthanMeetingPositive"] = CP_STATUS.FINISHED;

		}

		if (checkPoints ["SpokenToEthan"] == CP_STATUS.TRIGGERED) {
			NPC[] npcs = GameObject.FindObjectsOfType<NPC> ();
			NPC mayor = Array.Find (npcs, npc => npc.npcName == "Mayor");
			if (checkPoints ["SpokenToMayorFirst"] == CP_STATUS.TRIGGERED) {
				mayor.gameObject.GetComponent<NPCDialogue> ().SetCurrentDialogueBlock (2);
			} else {
				mayor.gameObject.GetComponent<NPCDialogue> ().SetCurrentDialogueBlock (3);
			}
			checkPoints["SpokenToEthan"] = CP_STATUS.FINISHED;
		}

		if (checkPoints ["MayorLeaveBeach"] == CP_STATUS.TRIGGERED) {
			Debug.Log("MayorLeaveBeach checkpoint - unimplemented");
		}
	}
}


public enum CP_STATUS {UNTRIGGERED, TRIGGERED, FINISHED}