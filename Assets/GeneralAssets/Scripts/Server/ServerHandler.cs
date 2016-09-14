using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ServerHandler : MonoBehaviour {

	private string secretKey = "BioDomeUnity16062908";

	private string url = "http://www.olivercoulson.com";

	private string playerUsername;
	public string PlayerUsername { get { return playerUsername; } }
	private string playerPassword;
	public string PlayerPassword { get { return playerPassword; } }

	public bool PlayerLoggedIn{get;set;}

	void Start ()
	{
		DontDestroyOnLoad(gameObject);
	}

	public string Login (string username, string password)
	{
		string outputMessage = "";

		StartCoroutine (LoginCoroutine (username, password, outputMessage));

		return outputMessage;
	}

	IEnumerator LoginCoroutine (string username, string password, string outputMessage)
	{
		WWWForm loginForm = new WWWForm ();
		loginForm.AddField ("myform_username", username);
		loginForm.AddField ("myform_password", password);
		loginForm.AddField ("myform_hash", secretKey);

		WWW www = new WWW (url + "/php/Login.php", loginForm);
		yield return www;

		if (www.error != null) {
			 outputMessage = "Error communicating with server: "+ www.error;
		} else {
			string wwwOutput = www.text;
			if (wwwOutput == "PASSWORD CORRECT") {
				playerUsername = username;
				playerPassword = password;
				PlayerLoggedIn = true;
			} 

			outputMessage = wwwOutput;
			
		}
	}


	public string RegisterNewUser (string username, string password, int question, string answer)
	{
		string response = "";
		StartCoroutine(RegisterCoroutine(username, password, question, answer, response));
		return response;

	}

	IEnumerator RegisterCoroutine (string username, string password, int question, string answer, string response)
	{

		WWWForm registerForm = new WWWForm ();
		registerForm.AddField ("myform_username", username);
		registerForm.AddField ("myform_password", password);
		registerForm.AddField ("myform_question", question);
		registerForm.AddField ("myform_answer", answer);
		registerForm.AddField ("myform_hash", secretKey);

		WWW www = new WWW (url + "/php/Register.php", registerForm);
		yield return www;

		if (www.error != null) {
			Debug.Log(www.error);
			response = www.error;
		} else {
			Debug.Log(www.text);
			response = www.text;
		}

//		Debug.Log("User: " + username + ", Password: " + password + ", Question: " + question + ", Answer:" + answer);
//		yield return new WaitForSeconds(2);
		//throw new NotImplementedException("Not Implemented yet");
	}

	public void SaveGame(string username, string password, List<Game> saveGames) {
		StartCoroutine(SaveCoroutine(username, password, saveGames));
	}

	IEnumerator SaveCoroutine(string username, string password, List<Game> saveGames) {
		throw new NotImplementedException("Not Implemented yet");
	}

	public void LoadGame(string username, string password) {
		StartCoroutine(LoadCoroutine(username, password));
	}

	IEnumerator LoadCoroutine(string username, string password) {
		throw new NotImplementedException("Not Implemented yet");
	}
}
