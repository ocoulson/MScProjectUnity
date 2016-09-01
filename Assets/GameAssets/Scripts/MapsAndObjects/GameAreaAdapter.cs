using UnityEngine;
using System.Collections;

public class GameAreaAdapter : MonoBehaviour {

	private GameArea area;

	public GameArea Area {
		get {
			return area;
		}
	}

	void Start ()
	{
		area = new GameArea(gameObject.name);
	}
}
