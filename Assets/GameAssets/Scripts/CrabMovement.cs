﻿using UnityEngine;
using System.Collections;

public class CrabMovement : MonoBehaviour {
	public float moveSpeed;
	public float runSpeed;
	public float dieTime;

	public float moveTime;
	public float waitTime;

	private bool IsMoving = false;
	private float waitCounter;
	private float moveCounter;

	private Rigidbody2D body;
	private Animator anim;
	private Vector2 currentDirection;
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
		if (IsMoving) {
			moveCounter -= Time.deltaTime;
			if (moveCounter < 0) {
				body.velocity = Vector2.zero;
				IsMoving = false;
				anim.SetBool ("IsMoving", false);
				waitCounter = waitTime + Random.Range(0, 10f);
				previousDirection = currentDirection;
			} else {
				body.velocity = (currentDirection * moveSpeed);

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
		IsMoving = true;
		moveCounter = moveTime;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			Vector3 pos = gameObject.transform.position;
			Vector3 playerPos = col.transform.position;
			Vector3 escapeDirection = (pos - playerPos) * runSpeed;
			IsMoving = true;
			anim.SetBool("IsMoving", true);
			moveCounter = moveTime * 2; 
		} 
	}
}
