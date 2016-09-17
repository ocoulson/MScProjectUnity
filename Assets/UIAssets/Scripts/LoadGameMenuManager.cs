using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class LoadGameMenuManager : MonoBehaviour {

	public GameObject title;
	public Canvas canvas;
	public GameObject updateButton;
	public GameObject backButton;
	public GameObject[] panels;

	public Text response;

	private ServerHandler server;
	private GameManager gameManager;
	// Use this for initialization
	void Start () {
		response.text = "";
		HidePanels();
		server = FindObjectOfType<ServerHandler>();
		gameManager = FindObjectOfType<GameManager>();
		StartCoroutine("Display");
	}

	public void CheckForSaveGames() {
		
		StartCoroutine(LoadCoroutine());
		//SaveLoad.Load();
		//Debug.Log(SaveLoad.ToString());

	}


	IEnumerator Display()
	{
		GameObject titleObject = Instantiate(title, title.GetComponent<RectTransform>().anchoredPosition,Quaternion.identity) as GameObject;
		titleObject.transform.SetParent(canvas.transform,false);
		yield return new WaitForSeconds(0.5f);
		GameObject updateButtonObject = Instantiate(updateButton, updateButton.GetComponent<RectTransform>().anchoredPosition,Quaternion.identity) as GameObject;
		updateButtonObject.transform.SetParent(canvas.transform,false);

		yield return new WaitForSeconds(0.5f);
	
		GameObject backButtonObject = Instantiate(backButton) as GameObject;
		backButtonObject.transform.SetParent(canvas.transform,false);
		yield return new WaitForSeconds(0.5f);
		StartCoroutine(LoadCoroutine());
	}

	IEnumerator LoadCoroutine ()
	{
		response.text = "";
		WWWForm loadForm = new WWWForm ();
		loadForm.AddField ("myform_username", server.PlayerUsername);
		loadForm.AddField ("myform_password", server.PlayerPassword);
		loadForm.AddField ("myform_hash", server.SecretKey);

		WWW www = new WWW (server.Url + "/php/LoadGame.php", loadForm);
		yield return www;

		if (www.error != null) {
			response.text = www.error;
		} else {
			string savedGameString = www.text;

			if (savedGameString == "No Saved Data found") {
				gameManager.SavedGames = new List<Game> ();
				response.text = savedGameString;
			} else {
				Debug.Log (savedGameString);
				byte[] binaryData = Convert.FromBase64String (savedGameString);
				MemoryStream stream = new MemoryStream (binaryData);
			

				BinaryFormatter bf = new BinaryFormatter ();
				gameManager.SavedGames = (List<Game>)bf.Deserialize (stream);
				foreach (Game g in gameManager.SavedGames) {
					Debug.Log(g.Player.Name);
				}
				if (gameManager.SavedGames.Count == 0) {
					yield return new WaitForSeconds (0.1f);
				}
				DisplayLoadGames();
			}
		}

	}



	void DisplayLoadGames ()
	{
		HidePanels();
		//List<Game> savedGames = SaveLoad.savedGames;
		List<Game> savedGames = gameManager.SavedGames;
		if (savedGames.Count == 0) {
			
			LoadGamePanel loadGamePanel = ActivatePanel(0);
			loadGamePanel.SetText ("No Saved Games Found");
			loadGamePanel.DisableImage ();
			return;
		}

		for (int i = 0; i < savedGames.Count; i++) {
			LoadGamePanel loadGamePanel = ActivatePanel(i);
			loadGamePanel.PanelGame = savedGames[i];
			loadGamePanel.SetText(savedGames[i].ToString());
			loadGamePanel.SetImage(Resources.LoadAll<Sprite>("Player/" + savedGames[i].Player.SpriteName)[4]);
		}

	}



	LoadGamePanel ActivatePanel (int i)
	{
		panels[i].SetActive(true);
		return panels[i].GetComponent<LoadGamePanel>();
	}
	void HidePanels ()
	{
		foreach (GameObject go in panels) {
			go.SetActive(false);
		}
	}

}
