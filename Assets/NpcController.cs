using UnityEngine;
using System.Collections;

public class NpcController : MonoBehaviour {

	public float speed;
	public float moveTime;
	public float waitTime;

	private Animator anim;
	private bool isMoving;
	private float moveCounter;
	private float waitCounter;
	private Rigidbody2D body;
	private Vector2[] directions;
	private int directionIndex;
	private Vector2 currentDirection;

			// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();

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
		if (isMoving) {
			moveCounter -= Time.deltaTime;
			if (moveCounter < 0) {
				body.velocity = Vector2.zero;
				isMoving = false;
				anim.SetBool("IsMoving", false);
				waitCounter = waitTime + Random.Range(0, 3f);
			} else {
				body.velocity = currentDirection * speed;
				anim.SetFloat("Xmove", body.velocity.x);
				anim.SetFloat("Ymove", body.velocity.y);
			}

		} else {
			waitCounter -= Time.deltaTime;
			if (waitCounter < 0) {
				ChooseDirection();
			}
		}
	
	}

	void ChooseDirection ()
	{
		if (directionIndex >= 3) {
			
			directionIndex = 0;
		} else {
			directionIndex ++;
		}
		currentDirection = directions[directionIndex];
		isMoving = true;
		anim.SetBool("IsMoving", true);
		moveCounter = moveTime;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		 isMoving = false;
		 waitCounter = waitTime;
		
	}
}
