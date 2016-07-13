using UnityEngine;
using System.Collections;

public class NpcController : MonoBehaviour {

	public float speed;
	public float moveTime;
	public float waitTime;

	public float movementBoxWidth;
	public float movementBoxHeight;

	private Vector2 startPos;

	private Vector2 maxPosition;
	private Vector2 minPosition;

	private Animator anim;
	private bool isMoving;
	private float moveCounter;
	private float waitCounter;
	private Rigidbody2D body;
	private Vector2[] directions;
	private int directionIndex;
	private Vector2 currentDirection;

	public bool movementEnabled;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		body = GetComponent<Rigidbody2D>();

		startPos = transform.position;

		maxPosition = new Vector2(startPos.x + movementBoxWidth, startPos.y + movementBoxHeight);
		minPosition = new Vector2(startPos.x - movementBoxWidth, startPos.y - movementBoxHeight);


		moveCounter = moveTime;
		waitCounter = waitTime;
		directions = new Vector2[]{
			Vector2.up, 
			Vector2.down,
			Vector2.left, 
			Vector2.right};
		movementEnabled = true;
	}

	private bool OutOfMovementBox ()
	{
		Vector2 currentPos = transform.position;
		return (currentPos.x > maxPosition.x || 
					currentPos.y > maxPosition.y || 
					currentPos.x < minPosition.x || 
					currentPos.y < minPosition.y);

	}

	void Update ()
	{
		if (movementEnabled) {
			if (isMoving) {
				moveCounter -= Time.deltaTime;
				if (moveCounter < 0) {
					body.velocity = Vector2.zero;
					isMoving = false;
					anim.SetBool ("IsMoving", false);
					waitCounter = waitTime + Random.Range (0, 3f);
				} else {
					if (OutOfMovementBox ()) {
						currentDirection = Vector2.zero - currentDirection;
					} else {
						body.velocity = currentDirection * speed;
						anim.SetFloat ("Xmove", body.velocity.x);
						anim.SetFloat ("Ymove", body.velocity.y);
					}
				}

			} else {
				waitCounter -= Time.deltaTime;
				if (waitCounter < 0) {
					ChooseDirection ();
				}
			}
		} else {
			body.velocity = Vector2.zero;
			isMoving = false;
			waitCounter = waitTime;
		}
	
	}

	void ChooseDirection ()
	{
		currentDirection = directions[Random.Range(0, directions.Length)];
		isMoving = true;
		anim.SetBool("IsMoving", true);
		moveCounter = moveTime;
	}

	public void TurnTowards (Vector2 target)
	{
		Vector2 relativeTarget = new Vector2(target.x - transform.position.x, target.y - transform.position.y);

		anim.SetFloat("Xmove", relativeTarget.x);
		anim.SetFloat("Ymove", relativeTarget.y);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (!(col.collider.gameObject.name == "Player")) {
			currentDirection = Vector2.zero - currentDirection;
		}
	}
}
