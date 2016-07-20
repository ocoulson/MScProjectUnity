using UnityEngine;
using System.Collections;

public class ToolRenderer : MonoBehaviour {

	private Player player;
	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		player = GetComponentInParent<Player>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

	}

	void Update ()
	{
		if (player.toolEquiped) {
			transform.GetChild (0).gameObject.SetActive (true);

			if (player.gameObject.GetComponent<SpriteRenderer> ().sprite.name.Contains ("Back") ||
			    player.gameObject.GetComponent<SpriteRenderer> ().sprite.name.Contains ("Left")) {

				spriteRenderer.sortingOrder = player.gameObject.GetComponent<SpriteRenderer> ().sortingOrder - 1;
			} else {
				spriteRenderer.sortingOrder = player.gameObject.GetComponent<SpriteRenderer> ().sortingOrder + 1;
			}


		} else {
			transform.GetChild (0).gameObject.SetActive (false);
		}

	}


}
