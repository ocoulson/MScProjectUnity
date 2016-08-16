using UnityEngine;
using System.Collections;

public class MockTool : Tool {

	public MockTool (string name)
	{
		toolName = name;
	}
	#region implemented abstract members of Tool

	public override InventoryItem Use ()
	{
		Debug.Log("MockTool Use");
		return new InventoryItem();
	}

	public override void OnTriggerEnter2DImpl (Collider2D col)
	{
		Debug.Log("MockTool OnTriggerEnter2DImpl");
	}

	public override void OnTriggerExit2DImpl (Collider2D col)
	{
		Debug.Log("MockTool OnTriggerExit2DImpl");
	}

	#endregion


	
}
