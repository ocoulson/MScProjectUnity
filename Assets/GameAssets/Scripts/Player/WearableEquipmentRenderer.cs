using UnityEngine;
using System.Collections;

public class WearableEquipmentRenderer : MonoBehaviour {

	private PlayerGameObject player;
	private SpriteRenderer spriteRenderer;
	private Animator equipAnim;
	private Animator playerAnim;


	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerGameObject>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sortingOrder = player.gameObject.GetComponent<SpriteRenderer> ().sortingOrder + 1;

		equipAnim = GetComponent<Animator>();
		playerAnim = player.gameObject.GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		equipAnim.SetBool("IsWalking", playerAnim.GetBool("IsWalking"));
		equipAnim.SetFloat("Input_x", playerAnim.GetFloat("Input_x"));
		equipAnim.SetFloat("Input_y", playerAnim.GetFloat("Input_y"));
	}
}
