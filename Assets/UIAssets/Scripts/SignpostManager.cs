using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignpostManager : MonoBehaviour {

	public GameObject signpostBox;
	public Text signText;

	public bool isActive;

	
	// Update is called once per frame
	void Update ()
	{
	}

	public void ShowSign (string newText)
	{	
		isActive = true;
		signpostBox.SetActive(true);
		signText.text = newText;

	}
	public void HideSign() {
		isActive = false;
		signpostBox.SetActive(false);
	}
}
 