using UnityEngine;
using System.Collections;

public class BirdMoveController : MonoBehaviour {

	
	public AnimationClip leftTweet;
	public AnimationClip rightTweet;
	public AnimationClip frontTweet;
	public float moveSpeed;
	public float dieTime = 5f;

	private Rigidbody2D body;
	private int idleState = 0;
	private bool moving = false;
	private Animator anim;


	void Start () {
		body = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!moving) {
			Idle ();
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Player") {
			Vector3 birdPos = gameObject.transform.position;
			Vector3 playerPos = col.transform.position;
			Vector3 velocity = (birdPos - playerPos) * moveSpeed;
			body.velocity = new Vector2 (velocity.x, velocity.y); 
			moving = true;
			if (velocity.x > 0) {
				anim.Play ("FlyRight");
			} else {
				anim.Play("FlyLeft");
			}
			StartCoroutine(SelfDestruct());
		} 

	}
	IEnumerator SelfDestruct ()
	{
		yield return new WaitForSeconds(dieTime);
		Destroy(gameObject);
	}
	void Idle ()
	{
		idleState = anim.GetInteger("IdleState");

		int IdleChangeChance = Random.Range (0, 1000);
		int TweetChance = Random.Range(0,1000);

		if (IdleChangeChance > 990) {
			idleState = Random.Range (0, 2);
			anim.SetInteger ("IdleState", idleState);
		} else if (TweetChance > 990){
			StartCoroutine(Tweet());
		}
	}



	IEnumerator Tweet ()
	{
		AnimationClip clip = null;
		if (idleState > 0) {
			clip = leftTweet;
		} else if (idleState < 0) {
			clip = rightTweet;
		} else {
			clip = frontTweet;
		}

		anim.Play(clip.name);
		yield return new WaitForSeconds(clip.length);
	}
}
