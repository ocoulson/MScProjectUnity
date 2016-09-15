using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserLabelPanel : MonoBehaviour {
	private ServerHandler server;
	public Text text;
	// Use this for initialization
	void Start () {
		server = FindObjectOfType<ServerHandler>();
		text.text = server.PlayerUsername;
	}
	

}
