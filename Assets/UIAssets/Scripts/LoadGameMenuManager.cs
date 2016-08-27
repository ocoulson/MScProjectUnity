using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class LoadGameMenuManager : MonoBehaviour {

	public GameObject title;
	public Canvas canvas;
	public GameObject backButton;
	public GameObject[] panels;


	// Use this for initialization
	void Start () {
		HidePanels();
		SaveLoad.Load();
		Debug.Log(SaveLoad.ToString());
		StartCoroutine("Display");
	}

	IEnumerator Display()
	{
		GameObject titleObject = Instantiate(title, title.GetComponent<RectTransform>().anchoredPosition,Quaternion.identity) as GameObject;
		titleObject.transform.SetParent(canvas.transform,false);
		yield return new WaitForSeconds(1f);
		DisplayLoadGames();
		GameObject backButtonObject = Instantiate(backButton) as GameObject;
		backButtonObject.transform.SetParent(canvas.transform,false);

	}

	void DisplayLoadGames ()
	{

		if (SaveLoad.savedGames.Count == 0) {
			
			LoadGamePanel loadGamePanel = ActivatePanel(0);
			loadGamePanel.SetText ("No Saved Games Found");
			loadGamePanel.DisableImage ();
			return;
		}

		for (int i = 0; i < SaveLoad.savedGames.Count; i++) {
			LoadGamePanel loadGamePanel = ActivatePanel(i);
			loadGamePanel.PanelGame = SaveLoad.savedGames[i];
			loadGamePanel.SetText(SaveLoad.savedGames[i].ToString());
			loadGamePanel.SetImage(Resources.LoadAll<Sprite>("Player/" + SaveLoad.savedGames[i].Player.SpriteName)[4]);
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
