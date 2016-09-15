using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class LoadGameMenuManager : MonoBehaviour {

	public GameObject title;
	public Canvas canvas;
	public GameObject updateButton;
	public GameObject backButton;
	public GameObject[] panels;


	// Use this for initialization
	void Start () {
		HidePanels();

		StartCoroutine("Display");
	}

	public void CheckForSaveGames() {
		string output = FindObjectOfType<ServerHandler>().LoadGame();
		Debug.Log(output);
		//SaveLoad.Load();
		//Debug.Log(SaveLoad.ToString());
		DisplayLoadGames();
	}

	IEnumerator Display()
	{
		HidePanels();
		GameObject titleObject = Instantiate(title, title.GetComponent<RectTransform>().anchoredPosition,Quaternion.identity) as GameObject;
		titleObject.transform.SetParent(canvas.transform,false);
		yield return new WaitForSeconds(0.5f);
		GameObject updateButtonObject = Instantiate(updateButton, updateButton.GetComponent<RectTransform>().anchoredPosition,Quaternion.identity) as GameObject;
		updateButtonObject.transform.SetParent(canvas.transform,false);
		yield return new WaitForSeconds(0.5f);
		DisplayLoadGames();
		yield return new WaitForSeconds(0.5f);
	
		GameObject backButtonObject = Instantiate(backButton) as GameObject;
		backButtonObject.transform.SetParent(canvas.transform,false);

	}

	void DisplayLoadGames ()
	{
		HidePanels();
		List<Game> savedGames = FindObjectOfType<ServerHandler>().SavedGames;
		//List<Game> savedGames = SaveLoad.savedGames;
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
