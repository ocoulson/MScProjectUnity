using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameArea  {

	private string areaName;

	public string AreaName {
		get {
			return areaName;
		}
	}

	public GameArea(string name) {
		areaName = name;
	}

}
