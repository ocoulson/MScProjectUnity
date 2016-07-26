using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolDisplayManager : MonoBehaviour {
	public GameObject toolBox;
	public Image toolImage;



	// Use this for initialization
	void Start () {
		toolBox.SetActive(false);
		toolImage.gameObject.SetActive(false);
	}

	public void SetToolImage(Sprite sprite) {
		toolImage.sprite = sprite;
		toolImage.gameObject.SetActive(true);
	}

	public void ShowToolImage() {
		toolBox.SetActive(true);
	}

	public void HideToolImage() {
		toolBox.SetActive(false);
	}
}
