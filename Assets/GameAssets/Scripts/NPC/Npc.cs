using UnityEngine;
using System.Collections;

public class Npc {

	private string name;
	public string Name {get {return name;} }

	private string spriteName;
	public string SpriteName {get {return spriteName;} }

	private DialogueBlock currentDialogueBlock;
	public DialogueBlock CurrentDialogueBlock {get {return currentDialogueBlock;}	set {currentDialogueBlock = value;}}

	private Vector2 currentStartPosition;
	public Vector2 CurrentStartPosition {get {return currentStartPosition;}	set {currentStartPosition = value;}}

	private float movementBoxSize;
	public float MovementBoxSize {get {return movementBoxSize;} }

	public Npc (string name, string spriteName, Vector2 currentStartPosition, float boxSize)
	{
		this.name = name;
		this.spriteName = spriteName;
		this.currentStartPosition = currentStartPosition;
		this.movementBoxSize = boxSize;

	}
}
