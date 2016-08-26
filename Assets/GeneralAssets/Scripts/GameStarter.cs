using UnityEngine;
using System.Collections;

public class GameStarter : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		FindObjectOfType<GameManager>().StartGame();
	}

}
