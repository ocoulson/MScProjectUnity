using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour {

	public GameObject instruction;

	public Image key;

	public Sprite blankKey;
	public Sprite blankSpace;

	public Text before;
	public Text keyText;
	public Text after;

	void Start() {
		instruction.SetActive(false);
	}


	public void DisplayInstruction (string beforeText, string keyText, string afterText)
	{
		if (keyText == "Space") {
			key.sprite = blankSpace;
			Vector2 scale = new Vector2(1f, key.rectTransform.localScale.y);
			key.rectTransform.localScale = scale;
		} else {
			key.sprite = blankKey;
			Vector2 scale = new Vector2(0.25f, key.rectTransform.localScale.y);
			key.rectTransform.localScale = scale;
		}
		this.keyText.text = keyText;
		before.text = beforeText;
		after.text = afterText;

		instruction.SetActive(true);
	}

	public void RemoveInstruction() {
		instruction.SetActive(false);
	}
}
