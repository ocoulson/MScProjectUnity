using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour {

	public GameObject instruction;
	public bool isActive { get { return instruction.activeInHierarchy;}}
	public Image key;

	public Sprite blankKey;
	public Sprite blankSpace;

	//First part of the instruciton (i.e. Press / Hold)
	public Text before;

	//The text on the key image (i.e. A, W, Space etc)
	public Text keyText;

	//The text after the key (i.e. to Read/ to Talk etc)
	public Text after;

	void Start() {
		instruction.SetActive(false);
	}


	public void ShowInstruction (string beforeText, string keyText, string afterText)
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

	public void HideInstruction() {
		instruction.SetActive(false);
	}

	public bool IsActive ()
	{
		return instruction.activeInHierarchy;
	}
}
