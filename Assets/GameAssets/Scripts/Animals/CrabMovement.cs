
using UnityEngine;
using System.Collections;

public class CrabMovement : NPC_MovementController {

	public float runSpeed;

	private Vector2 previousDirection = Vector2.zero;

	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();

		waitCounter = waitTime;
		moveCounter = moveTime;

	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateLoop();
	}

	public override void FinishMoving ()
	{
		base.FinishMoving();
		waitCounter = waitTime + Random.Range(0, 10f);
		previousDirection = currentDirection;
	}
	public override void ContinueMoving ()
	{
		body.velocity = (currentDirection * speed);
	}

	public override void ChooseDirection ()
	{
		if (previousDirection != Vector2.left && previousDirection != Vector2.right) {
			if (Random.Range (0, 1f) > 0.5f) {
				currentDirection = Vector2.right;
			} else {
				currentDirection = Vector2.left;
			}
		}
		else if (previousDirection == Vector2.left) {
			currentDirection = Vector2.right;
		} else {
			currentDirection = Vector2.left;
		}
		anim.SetBool("IsMoving", true);
		isMoving = true;
		moveCounter = moveTime + Random.Range(0, 1f);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			
			isMoving = true;
			anim.SetBool ("IsMoving", true);
			moveCounter = moveTime * 2; 
			body.velocity = Vector2.zero;

			//Move away from the player but not directly
			ChooseOppositeDirection(col.transform.position.x, transform.position.x);
		} else {
			isMoving = false;
			anim.SetBool("IsMoving", false);
			waitCounter = waitTime;
			body.velocity = Vector2.zero;
		}
	}

	void OnTriggerStay2D (Collider2D col)
	{
		//Layer 12 is the terrain layer
		if (col.gameObject.layer == 12) {
			ChooseOppositeDirection(col.transform.position.x, transform.position.x);
		}
	}

	void ChooseOppositeDirection(float colX, float thisX) {
		if (colX > thisX) {
				currentDirection = Vector2.left;
			} else {
				currentDirection = Vector2.right;
			}
	}
}
