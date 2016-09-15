using UnityEngine;
using System.Collections;

public class Logout : MonoBehaviour {
	private ServerHandler server;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		server = FindObjectOfType<ServerHandler>();
		levelManager = FindObjectOfType<LevelManager>();
	}
	

	public void LogoutUser() {
		server.Logout();
		levelManager.LoadLevel("Login");
	}
}
