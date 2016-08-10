using UnityEngine;
using System.Collections;
using System;


public class NPC : MonoBehaviour {

	public string npcName;
	public Sprite[] sprites { get; private set; }

	public float movementBoxSize;


	public Vector2 CurrentPosition {
		get {
			return GetComponent<NPC_BoxMovementController>().startPos;
		}
	}

	private SpriteRenderer spriteRenderer;

	private DialogueBlock currentDialogueBlock;

	public DialogueBlock CurrentDialogueBlock {
		get {
			return currentDialogueBlock;
		}
		set {
			currentDialogueBlock = value;
		}
	}


	// Use this for initialization
	void Start () {
		sprites = Resources.LoadAll<Sprite> ("NPCs/" + npcName);
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprites[4];

		GetComponent<NPC_BoxMovementController>().SetMovementBox(movementBoxSize,movementBoxSize);
	}

	void LateUpdate() {
		string spriteName = spriteRenderer.sprite.name;
		Sprite newSprite = Array.Find(sprites, sprite => sprite.name == spriteName);

		if (newSprite) {
			spriteRenderer.sprite = newSprite;
		} 
	}

}
