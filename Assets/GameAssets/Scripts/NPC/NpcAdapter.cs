using UnityEngine;
using System.Collections;
using System;


public class NpcAdapter : MonoBehaviour {

	private Npc npc;
	public Npc Npc {get {return npc;} set {npc = value;}}

	public string NpcName { get { return npc.Name; } }

	private Sprite[] sprites;
	public Sprite[] Sprites {get {return sprites;}	set {sprites = value;} }

	public float MovementBoxSize { get {return npc.MovementBoxSize; } }

	public Vector2 CurrentPosition {get {return npc.CurrentStartPosition;} }

	private SpriteRenderer spriteRenderer;

	public DialogueBlock CurrentDialogueBlock { get { return npc.CurrentDialogueBlock;} set {npc.CurrentDialogueBlock = value;} }


	// Use this for initialization
	void Start () {
		Sprites = Resources.LoadAll<Sprite> ("NPCs/" + npc.SpriteName);
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = Sprites[4];

		GetComponent<NPC_BoxMovementController>().SetMovementBox(MovementBoxSize,MovementBoxSize);
	}

	void LateUpdate() {
		string spriteName = spriteRenderer.sprite.name;
		Sprite newSprite = Array.Find(Sprites, sprite => sprite.name == spriteName);

		if (newSprite) {
			spriteRenderer.sprite = newSprite;
		} 
	}

}
