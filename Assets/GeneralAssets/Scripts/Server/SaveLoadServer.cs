using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System.Linq;

//Based on tutorial at:
//http://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934

public class SaveLoadServer: MonoBehaviour {

	public static List<Game> savedGames = new List<Game>();

	private static void AddNewSavedGame (Game game)
	{
		if (savedGames.Count >= 3) {
			Game earliest = savedGames.OrderBy(g => g.SaveTime).First();
			savedGames.Remove(earliest);
		} 
			
		game.SaveTime = System.DateTime.Now;
	    savedGames.Add(game);
		
	}

	public static void Save (Game game)
	{	
		Debug.Log(Application.persistentDataPath);
		//Add a Copy of the game, not the game itself, so multiple games can be saved not multiple references to the same one.
		AddNewSavedGame (game.Copy());
		BinaryFormatter bf = new BinaryFormatter ();
		string path = Application.persistentDataPath + "/savedGames.gd";
	    FileStream file = File.Create (path);
	    bf.Serialize(file, SaveLoad.savedGames);
	    string base64Data = Convert.ToBase64String(File.ReadAllBytes(path));
	    file.Close();


	}

	public static void Load ()
	{
		if (File.Exists (Application.persistentDataPath + "/savedGames.gd")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			SaveLoad.savedGames = (List<Game>)bf.Deserialize (file);
			file.Close ();
		} else {
			throw new FileNotFoundException("No file called 'savedGames.gd' found in Application.persistentDataPath/");
		}
	}

	public static new string ToString ()
	{
		StringBuilder builder = new StringBuilder ();
		foreach (Game g in savedGames) {
			builder.Append(g.ToString());
		}
		return builder.ToString();
	}

//	IEnumerator SaveCoroutine (string saveGameData)
//	{
//
//		WWWForm saveForm = new WWWForm ();
//		saveForm.AddField ("myform_username", playerUsername);
//		saveForm.AddField ("myform_password", playerPassword);
//		saveForm.AddField ("myform_savedata", saveGameData);
//
//		WWW www = new WWW (url + "php/SaveGame.php", saveForm);
//		yield return www;
//
//		if (www.error != null) {
//			Debug.LogError(www.error);
//			response = www.error;
//		} else {
//			Debug.Log(www.text);
//			response = www.text;
//		}
//
//	}
}
