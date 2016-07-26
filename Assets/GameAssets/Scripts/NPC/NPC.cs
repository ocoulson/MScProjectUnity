using UnityEngine;
using System.Collections;
using System;

public class NPC : MonoBehaviour {

	public string npcName;
	public Sprite[] sprites { get; private set; }


	private SpriteRenderer spriteRenderer;



	// Use this for initialization
	void Start () {
		sprites = Resources.LoadAll<Sprite> ("NPCs/" + npcName);
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprites[4];
	}

	void LateUpdate() {
		string spriteName = spriteRenderer.sprite.name;
		Sprite newSprite = Array.Find(sprites, sprite => sprite.name == spriteName);

		if (newSprite) {
			spriteRenderer.sprite = newSprite;
		} 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
