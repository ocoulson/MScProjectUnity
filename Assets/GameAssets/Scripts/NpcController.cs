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
				ChooseDirection();
			}
		}
	
	}

	void ChooseDirection ()
	{
		currentDirection = directions[Random.Range(0, directions.Length)];
		isMoving = true;
		anim.SetBool("IsMoving", true);
		moveCounter = moveTime;
	}

	//A method for dealing with things entering the trigger interaction zone collider (i.e. the player)
	//This method is effectively the OnTriggerEnter2D method for the NPC even though the NPC has a 
	//non-trigger collider.
	public void InteractionZoneEnter (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			Debug.Log("Interaction with " + col.gameObject.name);
		}
	}

	//A method for dealing with things entering the trigger interaction zone collider (i.e. the player)
	//This method is effectively the OnTriggerStay2D method for the NPC even though the NPC has a 
	//non-trigger collider.
	public void InteractionZoneStay (Collider2D col)
	{
		if (col.gameObject.name == "Player") {
			Debug.Log(col.gameObject.name + " is in the Interaction Zone");
		}
	}

}
