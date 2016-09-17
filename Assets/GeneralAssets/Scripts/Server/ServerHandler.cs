using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary; 

public class ServerHandler : MonoBehaviour {

	private List<Game> savedGames;

	public List<Game> SavedGames {
		get {
			if (savedGames == null) savedGames = new List<Game>();
			return savedGames;
		}
	}

	private string secretKey = "BioDomeUnity16062908";

	public string SecretKey {
		get {
			return secretKey;
		}
	}

	private string url = "http://www.olivercoulson.com";

	public string Url {
		get {
			return url;
		}
	}

	private string playerUsername;

	public string PlayerUsername {get {return playerUsername;}}

	private string playerPassword;

	public string PlayerPassword {get {return playerPassword;}}

	public bool PlayerLoggedIn{get;set;}
	private string response;

	void Start ()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void Logout() {
		playerUsername = "";
		playerPassword = "";
		savedGames = null;
		PlayerLoggedIn = false;
	}

	public void Login (string username, string password)
	{
		playerUsername = username;
		playerPassword = password;
		PlayerLoggedIn = true;
	}











}
