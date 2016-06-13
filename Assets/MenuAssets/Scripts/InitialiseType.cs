using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InitialiseType : MonoBehaviour {

	public float pause = 0.5f;
	public bool finished = false;

	string loadingDots = ".......";
    Text textComp;
    int textLength;

	void Start () {

		textComp = GetComponent<Text>();
      	textLength = textComp.text.Length;
        StartCoroutine(TypeText ());
	}

	IEnumerator TypeText () {
         foreach (char letter in loadingDots.ToCharArray()) {
             textComp.text += letter;
             yield return new WaitForSeconds (pause);
         }
     }

     void Update ()
	{
		if (textComp.text.Length == textLength + loadingDots.Length) {
			finished = true;
		} 
     }
	

}
