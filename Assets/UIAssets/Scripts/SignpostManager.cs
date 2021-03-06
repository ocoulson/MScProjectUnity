﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignpostManager : MonoBehaviour {
	private DialogueUIManager dialogue;
	public GameObject signpostBox;
	public Text signText;

	public bool isActive;

	public SignDirectionManager directionMan;


	void Start() {
		dialogue = FindObjectOfType<DialogueUIManager>();
	}
	public void ShowSignText (string newText)
	{	
		isActive = true;
		signpostBox.SetActive(true);
		signText.text = newText;
		signText.gameObject.SetActive(true);
		directionMan.gameObject.SetActive(false);
	}

	public void ShowSignDirections (string[] directionArrows, string[] directionText)
	{
		
		isActive = true;	
		signpostBox.SetActive(true);
		directionMan.gameObject.SetActive(true);
		//Make the main text blank so the directions can show up.
		signText.gameObject.SetActive(false);
		directionMan.ShowDirections(directionArrows, directionText);
	}

	public void HideSign ()
	{
		isActive = false;
		if (directionMan) {
			directionMan.HideAll();
		}
		signpostBox.SetActive(false);
	}

	void Update ()
	{
		if (dialogue.IsActive) {
			HideSign();
		}
	}
}
 