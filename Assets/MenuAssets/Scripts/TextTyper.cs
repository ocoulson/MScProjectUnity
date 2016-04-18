using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTyper : MonoBehaviour {

	public float letterPause = 0.01f;

	string messageText;
    Text textComp;
	// Use this for initialization
	void Start () {

		textComp = GetComponent<Text>();
        messageText = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText ());
	}

	IEnumerator TypeText () {
         foreach (char letter in messageText.ToCharArray()) {
             textComp.text += letter;
             yield return new WaitForSeconds (letterPause);
         }
     }
	

}
