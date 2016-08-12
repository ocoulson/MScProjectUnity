using UnityEngine;
using System.Collections;

public class ToolAdapter : MonoBehaviour {
	private Tool tool;

	public Tool Tool {
		get {
			return tool;
		}
		set {
			tool = value;
		}
	}

	public string ToolName {
		get { return tool.toolName; }
	}
	public Sprite Icon {
		get { return tool.Icon; }
		set { tool.Icon = value; }
	}

	private Animator anim;
	public Animator Anim {
		get { return anim; }
		set { anim = value; }
	}


	void Start() {

		anim = GetComponent<Animator>();
	}

	public void InitialiseSprite ()
	{
		GetComponent<SpriteRenderer>().sprite = tool.Sprites[0];
	}
	public InventoryItem Use ()
	{
		anim.SetTrigger ("UseTrigger");
		return tool.Use();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		tool.OnTriggerEnter2DImpl(col);
	}
	void OnTriggerExit2D(Collider2D col) 
	{
		tool.OnTriggerExit2DImpl(col);
	}
}
