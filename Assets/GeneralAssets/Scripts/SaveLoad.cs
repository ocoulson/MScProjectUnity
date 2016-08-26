﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System.Linq;

//Based on tutorial at:
//http://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934

public static class SaveLoad {

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
		AddNewSavedGame (game);
		BinaryFormatter bf = new BinaryFormatter ();
		if (File.Exists (Application.persistentDataPath + "/savedGames.gd")) {
			File.Delete(Application.persistentDataPath + "/savedGames.gd");
		}
	    FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
	    bf.Serialize(file, SaveLoad.savedGames);
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

}