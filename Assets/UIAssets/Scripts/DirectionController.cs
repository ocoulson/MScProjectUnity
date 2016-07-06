using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DirectionController : MonoBehaviour {
	public Image arrow;
	public Text text;

	public void SetDirectionArrow (Sprite sprite)
	{
		arrow.sprite = sprite;
	}		
	public void SetDirectionText (string directionText)
	{
		text.text = directionText;
	}

}
