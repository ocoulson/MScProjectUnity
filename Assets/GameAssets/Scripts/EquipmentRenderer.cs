using UnityEngine;
using System.Collections;

public class EquipmentRenderer : MonoBehaviour {

	private Player player;
	private SpriteRenderer spriteRenderer;


	// Use this for initialization
	void Start () {
		player = GetComponentInParent<Player>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sortingOrder = player.gameObject.GetComponent<SpriteRenderer> ().sortingOrder + 1;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player.hasBackpack) {

			gameObject.SetActive(true);
		} else {
			gameObject.SetActive(false);
		}
	}
}
