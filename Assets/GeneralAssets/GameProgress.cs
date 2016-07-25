using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameProgress : MonoBehaviour {

	public Dictionary<string, bool> checkPoints {get; private set;}
	private Player player;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
		checkPoints = new Dictionary<string,bool>();
		checkPoints.Add("SpokenToMayor", false);
		checkPoints.Add("SpokenToEthan", false);
		checkPoints.Add("FirstEthanMeetingPositive", false);
	}	
	// Update is called once per frame
	void Update ()
	{
		if (checkPoints ["FirstEthanMeetingPositive"]) {
			checkPoints ["SpokenToEthan"] = true;
			GameObject grabber = Instantiate(Resources.Load ("Prefabs/Tools/Grabber")) as GameObject;
			player.AddTool(grabber);

			GameObject backpack = Instantiate(Resources.Load ("Prefabs/Wearables/Backpack")) as GameObject;
			player.SetWearable(backpack);

			checkPoints["FirstEthanMeetingPositive"] = false;

		}
	}
}
