using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public bool wearingEquipment;

	public bool toolEquiped;

	public GameObject ToolSlot;
	public GameObject WearableSlot;

	public GameObject currentTool;
	public List<GameObject> tools { get; private set; }

	private ToolDisplayManager toolDisplay;

	void Start() {
		tools = new List<GameObject>();
		toolDisplay = FindObjectOfType<ToolDisplayManager>();
	}
	void Update ()
	{
		if (currentTool != null) {
			toolDisplay.ShowToolImage ();
			toolDisplay.SetToolImage(currentTool.GetComponent<Tool>().icon);
		}

	}
	public void AddTool (GameObject tool)
	{
		if (!tools.Contains (tool)) {
			tools.Add (tool);
			if (currentTool == null) {
				SetCurrentTool (tools.IndexOf (tool));
			}
		}
	}

	public void SetCurrentTool (int index)
	{
		if (index > tools.Count - 1) {
			Debug.Log ("Invalid tool choice");
		} else {
			currentTool = tools [index];
			ToolSlot.transform.DetachChildren();
			currentTool.transform.parent = ToolSlot.transform;
			currentTool.transform.position = ToolSlot.transform.position;
		}
	}



}
