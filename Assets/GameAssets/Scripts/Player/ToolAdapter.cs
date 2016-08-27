using UnityEngine;
using System.Collections;

public class ToolAdapter : MonoBehaviour {

	private Tool tool;
	public Tool Tool { get {return tool;} set {tool = value;} }


	public string ToolName {get { return tool.toolName; }}

	private Sprite[] sprites;
	public Sprite[] Sprites { get { return sprites; } set { sprites = value; } }

	private Sprite icon;
	public Sprite Icon { get {return icon;} set { icon = value; } }


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
		Sprites = Resources.LoadAll<Sprite>("Equipment/" + ToolName);
		Icon = Resources.Load<Sprite>("Equipment/"+ ToolName + "Icon"); 

		GetComponent<SpriteRenderer>().sprite = Sprites[0];
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
