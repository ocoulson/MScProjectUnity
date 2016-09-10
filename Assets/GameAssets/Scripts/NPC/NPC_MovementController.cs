using UnityEngine;
using System.Collections;

public class NPC_MovementController : MonoBehaviour {

	public float speed;
	public float moveTime;
	public float waitTime;

	public float moveCounter { get; set; }
	public float waitCounter { get; set; }

	public Rigidbody2D body{get;set;}
	public Animator anim{get;set;}

	public Vector2[] directions{get;set;}
	public Vector2 currentDirection{get;set;}

	public bool movementEnabled;
	public bool isMoving{get;set;}

	void Start() {
		Initialise();
	}

	public virtual void Initialise() {
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		moveCounter = moveTime;
		waitCounter = waitTime;

		directions = new Vector2[]{
			Vector2.up, 
			Vector2.down,
			Vector2.left, 
			Vector2.right};
	}

	void Update ()
	{
		UpdateLoop();
	}
	public void StandStill ()
	{
		body.velocity = Vector2.zero;
		isMoving = false;
		waitCounter = waitTime;
	}
	public void UpdateLoop ()
	{
		if (!movementEnabled) {
			StandStill();
			return;
		}

		if (isMoving) {
			moveCounter -= Time.deltaTime;
			if (moveCounter < 0) {
				FinishMoving();
			} else {
				ContinueMoving();
			}

		} else {
			waitCounter -= Time.deltaTime;
			if (waitCounter < 0) {
				ChooseDirection();
			}
		}

	}
	public virtual void ContinueMoving ()
	{
		body.velocity = currentDirection * speed;
		anim.SetFloat("Xmove", body.velocity.x);
		anim.SetFloat("Ymove", body.velocity.y);
	}
	public virtual void FinishMoving() {
		body.velocity = Vector2.zero;
		isMoving = false;
		anim.SetBool("IsMoving", false);
		waitCounter = waitTime + Random.Range(0, 3f);
	}

	public virtual void ChooseDirection() {
		currentDirection = directions[Random.Range(0,4)];
		isMoving = true;
		anim.SetBool("IsMoving", true);
		moveCounter = moveTime + Random.Range(0, 1f);
	}

	public void TurnTowards (Vector2 target)
	{
		
		Vector2 relativeTarget = new Vector2(target.x - transform.position.x, target.y - transform.position.y);

		anim.SetFloat("Xmove", relativeTarget.x);
		anim.SetFloat("Ymove", relativeTarget.y);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		 isMoving = false;
		 waitCounter = waitTime;
		
	}
}
