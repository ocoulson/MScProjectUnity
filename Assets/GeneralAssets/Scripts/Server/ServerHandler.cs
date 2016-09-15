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

	private string url = "http://www.olivercoulson.com";

	private string playerUsername;
	public string PlayerUsername { get { return playerUsername; } }
	private string playerPassword;
	public string PlayerPassword { get { return playerPassword; } }

	public bool PlayerLoggedIn{get;set;}
	private string response;

	void Start ()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void Logout() {
		playerUsername = "";
		playerPassword = "";

		PlayerLoggedIn = false;
	}

	public string Login (string username, string password)
	{

		StartCoroutine (LoginCoroutine (username, password));
		string output = response;
		response = "";
		return output;
	}

	IEnumerator LoginCoroutine (string username, string password)
	{
		WWWForm loginForm = new WWWForm ();
		loginForm.AddField ("myform_username", username);
		loginForm.AddField ("myform_password", password);
		loginForm.AddField ("myform_hash", secretKey);

		WWW www = new WWW (url + "/php/Login.php", loginForm);
		yield return www;

		if (www.error != null) {
			 response = "Error communicating with server: "+ www.error;
		} else {
			string wwwOutput = www.text;
			if (wwwOutput == "PASSWORD CORRECT") {
				playerUsername = username;
				playerPassword = password;
				PlayerLoggedIn = true;
			} 

			response = wwwOutput;
			
		}
	}


	public string RegisterNewUser (string username, string password, int question, string answer)
	{
		StartCoroutine(RegisterCoroutine(username, password, question, answer));
		string output = response;
		response = "";
		return output;

	}

	IEnumerator RegisterCoroutine (string username, string password, int question, string answer)
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
			Debug.LogError(www.error);
			response = www.error;
		} else {
			Debug.Log(www.text);
			response = www.text;
		}
	
	}

	public string SaveGame(Game game) {
		AddNewSavedGame(game.Copy());
		BinaryFormatter bf = new BinaryFormatter ();
		//string path = Application.persistentDataPath + "/savedGames.gd";
	    //FileStream file = File.Create (path);
	    MemoryStream stream = new MemoryStream();
	    bf.Serialize(stream, SaveLoad.savedGames);

	    string base64Data = Convert.ToBase64String(stream.ToArray());
	    //file.Close();

		StartCoroutine(SaveCoroutine(base64Data));
		string output = response;
		response = "";
		return output;
	}

	IEnumerator SaveCoroutine (string saveGameData)
	{

		WWWForm saveForm = new WWWForm ();
		saveForm.AddField ("myform_username", playerUsername);
		saveForm.AddField ("myform_password", playerPassword);
		saveForm.AddField ("myform_savedata", saveGameData);
		saveForm.AddField ("myform_hash", secretKey);

		WWW www = new WWW (url + "/php/SaveGame.php", saveForm);
		yield return www;

		if (www.error != null) {
			Debug.LogError(www.error);
			response = www.error;
		} else {
			Debug.Log(www.text);
			response = www.text;
		}

	}

	public string LoadGame() {
		Debug.Log("LoadGameCalled");
		StartCoroutine(LoadCoroutine());
		string output = response;
		response = "";
		return output;
	}

	IEnumerator LoadCoroutine ()
	{
		Debug.Log("Coroutine started");
		WWWForm loadForm = new WWWForm ();
		loadForm.AddField ("myform_username", playerUsername);
		loadForm.AddField ("myform_password", playerPassword);
		loadForm.AddField ("myform_hash", secretKey);

		WWW www = new WWW (url + "/php/LoadGame.php", loadForm);
		yield return www;
		Debug.Log(www.ToString());
		if (www.error != null) {
			Debug.LogError (www.error);
			response = www.error;
		} else {
			string savedGameString = www.text;
			Debug.Log(savedGameString);
			if (savedGameString == "No Saved Data found") {
				savedGames = new List<Game> ();
				response = savedGameString;
			} else {

				byte[] binaryData = Convert.FromBase64String(savedGameString);
				Debug.Log(binaryData);
				//string path = Application.persistentDataPath + "/savedGames.gd";
				//File.Create(path);

				//File.WriteAllBytes(path, binaryData);

				//FileStream file = File.OpenRead(path);
				MemoryStream stream = new MemoryStream(binaryData);
				BinaryFormatter bf = new BinaryFormatter();
				savedGames = (List<Game>)bf.Deserialize (stream);
			
				//file.Close ();
			}
		}

	}




	private void AddNewSavedGame (Game game)
	{
		if (SavedGames.Count >= 3) {
			Game earliest = SavedGames.OrderBy(g => g.SaveTime).First();
			SavedGames.Remove(earliest);
		} 
			
		game.SaveTime = System.DateTime.Now;
	    savedGames.Add(game);
		
	}
}
