using UnityEngine;
using System.Collections;

public class ButterflyMovement : MonoBehaviour {

	public float speed;
	public float moveTime;
	public float waitTime;

	private Animator anim;
	private bool isMoving;
	private float moveCounter;
	private float waitCounter;
	private Rigidbody2D body;
	private Vector2[] directions;
	private Vector2 direction;

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
	
	// Update is called once per frame
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
				body.velocity = direction * speed;
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

	void ChooseDirection() {
		direction = directions[Random.Range(0,4)];
		isMoving = true;
		moveCounter = moveTime + Random.Range(0, 1f);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		 isMoving = false;
		 waitCounter = waitTime;
		
	}
}
