using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {
	private PlayerAdapter player;
	private Rigidbody2D rBody;
	private Animator anim;

	public bool IsMoving { get; private set; }

	public  bool movementEnabled;
	void Start () {
		rBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		player = GetComponent<PlayerAdapter>();
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

		bool left = Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow);
		bool right = Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow);
		bool up = Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow);
		bool down = Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow);

		//Choose the movement, diagonal movement is disabled.
		if (left) {
			xInput = -1f;
		} else if (right) {
			xInput = 1f;
		}
		if (up) {
			yInput = 1f;
		} else if (down) {
			yInput = -1f;
		}

		Vector2 direction_vector = new Vector2 (xInput, yInput);

		//move between the idle and moving blend trees in the animation controller
		if (direction_vector != Vector2.zero) {

			anim.SetBool ("IsWalking", true);
			anim.SetFloat ("Input_x", direction_vector.x);
			anim.SetFloat ("Input_y", direction_vector.y);

			if (direction_vector.x != 0 && direction_vector.y != 0) {
				speed = speed * 0.75f;
			}
			Vector2 newPosition = rBody.position + direction_vector * speed * Time.deltaTime;

			rBody.MovePosition (newPosition);
			player.UpdatePlayerPosition(newPosition);
	
		} else {

			anim.SetBool ("IsWalking", false);

		}


	}

	public void EnableMovement() {
		movementEnabled = true;
	}
	public void DisableMovement ()
	{
		movementEnabled = false;
	}

}
