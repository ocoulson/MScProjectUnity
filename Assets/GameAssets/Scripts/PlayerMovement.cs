using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rBody;
	Animator anim;
	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 movement_vector = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (movement_vector != Vector2.zero) {
			anim.SetBool ("IsWalking", true);
			anim.SetFloat("Input_x", movement_vector.x);
			anim.SetFloat("Input_y", movement_vector.y);
		} else {
			anim.SetBool ("IsWalking", false);
		}

		rBody.MovePosition(rBody.position + movement_vector * Time.deltaTime);
	}

}
