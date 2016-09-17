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

		StartCoroutine(LoginCoroutine(username.text, password.text));

	}

	IEnumerator LoginCoroutine (string username, string password)
	{
		WWWForm loginForm = new WWWForm ();
		loginForm.AddField ("myform_username", username);
		loginForm.AddField ("myform_password", password);
		loginForm.AddField ("myform_hash", server.SecretKey);

		WWW www = new WWW (server.Url + "/php/Login.php", loginForm);
		yield return www;

		if (www.error != null) {
			response.text = "Error communicating with server: " + www.error;
		} else {
			string wwwOutput = www.text;
			if (wwwOutput == "PASSWORD CORRECT") {

				server.Login (username, password);

				response.text = wwwOutput;
				yield return new WaitForSeconds(0.5f);
				FindObjectOfType<LevelManager> ().LoadLevel ("Initialising");
			
			} else {
				response.text = wwwOutput;
			}

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
