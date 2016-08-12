using UnityEngine;
using System.Collections;

public class ToolRenderer : MonoBehaviour {

	private PlayerView player;
	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		player = GetComponentInParent<PlayerView>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

	}

	void Update ()
	{
		if (player.gameObject.GetComponent<SpriteRenderer> ().sprite.name.Contains ("Back") ||
		    player.gameObject.GetComponent<SpriteRenderer> ().sprite.name.Contains ("Left")) {

			spriteRenderer.sortingOrder = player.gameObject.GetComponent<SpriteRenderer> ().sortingOrder - 1;
		} else {
			spriteRenderer.sortingOrder = player.gameObject.GetComponent<SpriteRenderer> ().sortingOrder + 1;
		}


	}


}
