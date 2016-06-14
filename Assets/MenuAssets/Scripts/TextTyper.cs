using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextTyper : MonoBehaviour {

	public float letterPause = 0.01f;
	public AudioClip[] sounds;

	private AudioSource source { get { return GetComponent<AudioSource> (); } }
	private string messageText;
    private Text textComp;
	
	void Start () {
		gameObject.AddComponent<AudioSource>();
		source.volume = 0.5f;
		textComp = GetComponent<Text>();
        messageText = textComp.text;
        textComp.text = "";
        StartCoroutine(TypeText ());
	}

	IEnumerator TypeText () {
         foreach (char letter in messageText.ToCharArray()) {
             textComp.text += letter;
			 source.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
             yield return new WaitForSeconds (letterPause);
         }
     }
	

}
