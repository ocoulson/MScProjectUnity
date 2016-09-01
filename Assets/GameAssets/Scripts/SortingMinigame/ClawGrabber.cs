using UnityEngine;
using System.Collections;

public class ClawGrabber : MonoBehaviour {

	private Animator anim;
	private CircleCollider2D attachedCollider;

	private GameObject grabbedObject;

	private bool objectGrabbed;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		attachedCollider = GetComponent<CircleCollider2D>();
	}

	public void EnableCollider ()
	{
		attachedCollider.enabled = true;
	}

	public void DisableCollider ()
	{
		attachedCollider.enabled = false;
	}
	public void Grab() {
		anim.SetTrigger("GrabTrigger");
	}

	public void Release ()
	{
		if (objectGrabbed) {
			grabbedObject.layer = 20;
			grabbedObject.transform.parent = null;
			grabbedObject.AddComponent<Rigidbody2D>();
			objectGrabbed = false;
			anim.SetBool("objectGrabbed", false);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		col.transform.SetParent(gameObject.transform);
		grabbedObject = col.gameObject;
		Destroy(grabbedObject.GetComponent<Rigidbody2D>());
		objectGrabbed = true;
		anim.SetBool("objectGrabbed", true);

	}

	void OnTriggerStay2D (Collider2D col)
	{
		//col.transform.position = transform.position + new Vector3(0, -0.75f,0);

		col.transform.localScale = new Vector2( col.transform.localScale.x * transform.localScale.x, col.transform.localScale.y * transform.localScale.y);
	}


}
