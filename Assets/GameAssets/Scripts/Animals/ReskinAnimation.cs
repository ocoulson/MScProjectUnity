using UnityEngine;
using System.Collections;
using System;

public class ReskinAnimation : MonoBehaviour {
	
	public string folder;
	public string SpriteSheetName;
	void LateUpdate ()
	{
		Sprite[] subSprites = Resources.LoadAll<Sprite> (folder + "/" + SpriteSheetName);

		foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {
			string spriteName = renderer.sprite.name;
			Sprite newSprite = Array.Find(subSprites, sprite => sprite.name == spriteName);

			if (newSprite) {
				renderer.sprite = newSprite;
			}
		}
	}

	public void SetSpriteSheetName (string name)
	{
		SpriteSheetName = name;
	}

	public string GetSpriteSheetName() {
		return SpriteSheetName;
	}
}
