using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadGamePanel : MonoBehaviour {
	public Text panelText;
	public Image panelImage;

	private Game panelGame;

	public Game PanelGame {
		get {
			return panelGame;
		}
		set {
			panelGame = value;
		}
	}

	public void SetText (string newText)
	{  
		panelText.text = newText;
	}

	public void SetImage(Sprite newImage) {
		panelImage.gameObject.SetActive(true);
		panelImage.sprite = newImage;
	}

	public void DisableImage ()
	{
		panelImage.gameObject.SetActive(false);
	}

	public void LoadGame ()
	{
		if (panelGame != null) {
			LevelManager manager = FindObjectOfType<LevelManager> ();
			manager.LoadGame (panelGame);
		}
	}
}
