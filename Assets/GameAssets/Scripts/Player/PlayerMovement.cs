using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
	
	private Rigidbody2D rBody;
	private Animator anim;
	private Animator[] childAnimators;

	public bool IsMoving { get; private set; }

	public  bool movementEnabled;
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	
	}



	void Update ()
	{
		

		if (!movementEnabled) {
			anim.SetBool ("IsWalking", false);
			rBody.velocity = Vector2.zero;
			return;
		}

		float speed;
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 1.25f;
			anim.speed = 1.5f;
		} else {
			speed = 1f;	
			anim.speed = 1.25f;
		}


		float xInput = 0;
		float yInput = 0;

		bool left = Input.GetKey (KeyCode.A);
		bool right = Input.GetKey (KeyCode.D);
		bool up = Input.GetKey (KeyCode.W);
		bool down = Input.GetKey (KeyCode.S);

		//Choose the movement, diagonal movement is disabled.
		if (left) {
			xInput = -1f;
		} else if (right) {
			xInput = 1f;
		} else if (up) {
			yInput = 1f;
		} else if (down) {
			yInput = -1f;
		}

		Vector2 movement_vector = new Vector2 (xInput, yInput) * speed;

		//move between the idle and moving blend trees in the animation controller
		if (movement_vector != Vector2.zero) {

			anim.SetBool ("IsWalking", true);
			anim.SetFloat ("Input_x", movement_vector.x);
			anim.SetFloat ("Input_y", movement_vector.y);
	
		} else {

			anim.SetBool ("IsWalking", false);

		}

		rBody.MovePosition (rBody.position + movement_vector * Time.deltaTime);
	

	}

	public void EnableMovement() {
		movementEnabled = true;
	}
	public void DisableMovement ()
	{
		movementEnabled = false;
	}

}
