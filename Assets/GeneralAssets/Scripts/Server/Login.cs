using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour {
	public Text response;

	public InputField username;
	public InputField password;

	private ServerHandler server;
	// Use this for initialization
	void Start () {
		response.text = "";
		server = FindObjectOfType<ServerHandler>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			if (username.isFocused) {
				password.Select();
			}
			if (password.isFocused) {
				username.Select();
			}
		}
	}

	public void LoginUser ()
	{
		if (!CheckInput ()) {
			return;
		}

		string loginOutput = server.Login (username.text, password.text);

		response.text = loginOutput;

		if (server.PlayerLoggedIn) {
			FindObjectOfType<LevelManager>().LoadLevel("Initialising");
		}
	}

	private bool CheckInput() {
		if (username.text == "") {
			response.text = "Please enter a Username";
			return false;
		}
		if (username.text.Length > 20) {
			response.text = "Username is too long. It must be less than 20 characters";
			username.text = "";
			return false;
		}
		if (password.text == "") {
			response.text = "Please enter a Password";
			return false;
		}
		if (password.text.Length > 20) {
			response.text = "Password is too long. It must be less than 20 characters";
			password.text = "";
			return false;
		}

		Regex mainRegex = new Regex ("^[a-zA-Z0-9]*$");

		if (!mainRegex.IsMatch (username.text)) {
			response.text = "Username can only contain letters a-z, A-Z and numbers 0-9";
			username.text = "";
			return false;
		}
		if (!mainRegex.IsMatch (password.text)) {
			response.text = "Password can only contain letters a-z, A-Z and numbers 0-9";
			password.text = "";
			return false;
		}

		//TODO: Add check for secret answer - SQL injection
		response.text = "";
		return true;
	}
}
