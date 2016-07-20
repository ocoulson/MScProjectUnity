using UnityEngine;
using System.Collections;

public class ToolUse : MonoBehaviour {

	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();	
	}

	public void PlayClip() {
		anim.SetTrigger("UseTrigger");
	}
}
