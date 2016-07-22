using UnityEngine;
using System.Collections;

public class Tool : MonoBehaviour {
	public string toolName;
	public Sprite sprite;
	public Sprite icon;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
		gameObject.GetComponent<SpriteRenderer>().sprite = sprite;	

	}

	public void Use() {
		anim.SetTrigger("UseTrigger");
	}
}
