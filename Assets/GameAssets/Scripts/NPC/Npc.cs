using UnityEngine;
using System.Collections;

[System.Serializable]
public class Npc {

	private string name;
	public string Name {get {return name;} }

	private string spriteName;
	public string SpriteName {get {return spriteName;} }

	private DialogueBlock currentDialogueBlock;
	public DialogueBlock CurrentDialogueBlock {get {return currentDialogueBlock;}	set {currentDialogueBlock = value;}}

	private Vector2Serializable currentStartPosition;
	public Vector2 CurrentStartPosition {get {return currentStartPosition.Vector2;}	set {currentStartPosition.Vector2 = value;}}

	private float movementBoxSize;
	public float MovementBoxSize {get {return movementBoxSize;} }

	private bool canMove;

	public bool CanMove {
		get {
			return canMove;
		}
		set {
			canMove = value;
		}
	}

	public Npc (string name, string spriteName, Vector2 currentStartPosition, float boxSize, bool canMove)
	{
		this.name = name;
		this.spriteName = spriteName;
		this.currentStartPosition = new Vector2Serializable(currentStartPosition);
		this.movementBoxSize = boxSize;
		this.canMove = canMove;

	}

	public Npc Copy ()
	{
		Npc copy = new Npc (name, spriteName, CurrentStartPosition, MovementBoxSize, canMove);
		if (CurrentDialogueBlock != null) {
			copy.CurrentDialogueBlock = CurrentDialogueBlock.Copy ();
		}
		return copy;
	}
}
