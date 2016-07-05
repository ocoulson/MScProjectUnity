using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SignpostManager : MonoBehaviour {

	public GameObject signpostBox;
	public Text signText;

	public bool isActive;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isActive && Input.GetKeyDown (KeyCode.Space)) {
			signpostBox.SetActive(false);
		}
	}

	public void ShowSign (string newText)
	{	
			isActive = true;
			signpostBox.SetActive(true);
			signText.text = newText;

	}
}
 