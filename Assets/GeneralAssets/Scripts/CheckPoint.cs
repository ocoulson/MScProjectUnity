using UnityEngine;
using System.Collections;
[System.Serializable]
public class CheckPoint {

	private string name;

	public string Name {
		get {
			return name;
		}
		private set {
			name = value;
		}
	}

	private CP_STATUS status;

	public CP_STATUS Status {
		get {
			return status;
		}
		set {
			status = value;
		}
	}

	public CheckPoint (string name) {
		Name = name;
		Status = CP_STATUS.UNTRIGGERED;
	}
}
