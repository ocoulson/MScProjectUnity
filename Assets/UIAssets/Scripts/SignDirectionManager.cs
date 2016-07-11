using UnityEngine;
using System.Collections;

public class SignDirectionManager : MonoBehaviour {

	public GameObject[] directions = new GameObject[4];
	public Sprite left;
	public Sprite right;
	public Sprite up;
	public Sprite down;

	private void ShowOneDirection (string directionArrow, string text, int index )
	{
		GameObject direction = directions [index];
		DirectionController controller = direction.GetComponent<DirectionController> ();
		if (directionArrow == "Left") {
			controller.SetDirectionArrow (left);
		} else if (directionArrow == "Right") {
			controller.SetDirectionArrow (right);
		} else if (directionArrow == "Up") {
			controller.SetDirectionArrow (up);
		} else if (directionArrow == "Down") {
			controller.SetDirectionArrow (down);
		} else {
			Debug.Log("Incorrect directionArrow string");
		}
		controller.SetDirectionText(text);

		direction.SetActive(true);
	}

	public void ShowDirections (string[] directionArrows, string[] texts)
	{	
		
		if (directionArrows.Length != texts.Length) {
			Debug.LogError ("ShowDirections method parameters are not equal length");
		} else if (directionArrows.Length == 0) {
			Debug.LogError ("ShowDirections method parameters empty");
		} else if (directionArrows.Length > 4) {
			Debug.LogError ("ShowDirections method parameters too long");
		} else {
			//Based on the given directions and texts, show the directions on the sign
			//If there are 1 or 2 directionArrows, the 2nd and 3rd down will be the ones shown
			//Otherwise it will start at the top.
			HideAll();
			switch (directionArrows.Length) {
				case 1: ShowOneDirection(directionArrows[0], texts[0], 1);
						break;
				case 2: ShowOneDirection(directionArrows[0], texts[0], 1);
						ShowOneDirection(directionArrows[1], texts[1], 2);
						break;
				case 3: ShowOneDirection(directionArrows[0], texts[0], 0);
						ShowOneDirection(directionArrows[1], texts[1], 1);
						ShowOneDirection(directionArrows[2], texts[2], 2);
						break;
				case 4: ShowOneDirection(directionArrows[0], texts[0], 0);
						ShowOneDirection(directionArrows[1], texts[1], 1);
						ShowOneDirection(directionArrows[2], texts[2], 2);
						ShowOneDirection(directionArrows[3], texts[3], 3);
						break;

			}
		} 

	}


	public void HideAll ()
	{
		foreach (GameObject g in directions) {
			g.SetActive(false);
		}
	}
}
