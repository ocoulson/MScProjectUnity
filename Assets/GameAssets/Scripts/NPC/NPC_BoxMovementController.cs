using UnityEngine;
using System.Collections;

public class NPC_BoxMovementController : NPC_MovementController {

	public float movementBoxWidth;
	public float movementBoxHeight;

	public Vector2 startPos{get;set;}

	public Vector2 maxPosition{get;set;}
	public Vector2 minPosition{get;set;}
	 
	// Use this for initialization
	void Start () {
		Initialise();
	}
	public override void Initialise ()
	{
		base.Initialise();


		startPos = transform.position;

		SetMovementBox(movementBoxWidth, movementBoxHeight);
	}

	// Update is called once per frame
	void Update () {
		UpdateLoop();
	}

	public override void ContinueMoving ()
	{
		if (OutOfMovementBox ()) {
			currentDirection = Vector2.zero - currentDirection;
		} 
		body.velocity = currentDirection * speed;
		anim.SetFloat ("Xmove", body.velocity.x);
		anim.SetFloat ("Ymove", body.velocity.y);
		
	}
	private bool OutOfMovementBox ()
	{
		Vector2 currentPos = transform.position;
		return (currentPos.x > maxPosition.x || 
					currentPos.y > maxPosition.y || 
					currentPos.x < minPosition.x || 
					currentPos.y < minPosition.y);


	}
	void OnCollisionEnter2D (Collision2D col)
	{
		if (!(col.collider.gameObject.name == "Player")) {
			currentDirection = Vector2.zero - currentDirection;
		}
	}

	public void SetMovementBox (float width, float height)
	{
		movementBoxWidth = width;
		movementBoxHeight = height;

		maxPosition = new Vector2(startPos.x + movementBoxWidth, startPos.y + movementBoxHeight);
		minPosition = new Vector2(startPos.x - movementBoxWidth, startPos.y - movementBoxHeight);
	}
}
