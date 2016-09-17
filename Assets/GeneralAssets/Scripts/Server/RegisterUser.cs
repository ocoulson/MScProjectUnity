using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class RegisterUser : MonoBehaviour {

	public Text response;

	public InputField username;
	public InputField password;
	public InputField passwordConf;
	public InputField secretAnswer;

	public Dropdown secretQuestion;

	private ServerHandler server;

	void Start ()
	{
		response.text = "";
		server = FindObjectOfType<ServerHandler>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab)) {
			if (username.isFocused) {
				password.Select();
			}
			if (password.isFocused) {
				passwordConf.Select();
			}
			if (passwordConf.isFocused) {
				secretAnswer.Select();
			}
			if (secretAnswer.isFocused) {
				username.Select();
			}
		}
	}
	
	public void OnSubmit ()
	{	
		if (!CheckInput ()) {
			return;
		}

		StartCoroutine( RegisterCoroutine(username.text, password.text, secretQuestion.value, secretAnswer.text));
	}

	IEnumerator RegisterCoroutine (string username, string password, int question, string answer)
	{

		WWWForm registerForm = new WWWForm ();
		registerForm.AddField ("myform_username", username);
		registerForm.AddField ("myform_password", password);
		registerForm.AddField ("myform_question", question);
		registerForm.AddField ("myform_answer", answer);
		registerForm.AddField ("myform_hash", server.SecretKey);

		WWW www = new WWW (server.Url + "/php/Register.php", registerForm);
		yield return www;

		if (www.error != null) {
			Debug.LogError(www.error);
			response.text = www.error;
		} else {
			Debug.Log(www.text);
			response.text = www.text;
		}
	
	}

	private bool CheckInput ()
	{
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
		if (passwordConf.text == "") {
			response.text = "Please confirm the Password";
			return false;
		}
		if (secretAnswer.text == "") {
			response.text = "Please provide an answer to the secret question";
			return false;
		}
		if (secretAnswer.text.Length > 20) {
			response.text = "Secret answer is too long. It must be less than 30 characters";
			secretAnswer.text = "";
			return false;
		}
		if (password.text != passwordConf.text) {
			response.text = "Passwords don't match!";
			password.text = "";
			passwordConf.text = "";
			return false;
		}

		if (secretQuestion.value == 0) {
			response.text = "Please select a secret question";
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
