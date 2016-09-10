using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MinigameHolder : MonoBehaviour {

	public Text text;
	public Image icon;
		
	public void SetText (string newText)
	{
		text.text = newText;
	}

	public void SetImage(Sprite newImage) 
	{
		icon.sprite = newImage;
	}

	public void LoadMinigame (string name)
	{
		FindObjectOfType<LevelManager>().LoadLevel(name);
	}
}
