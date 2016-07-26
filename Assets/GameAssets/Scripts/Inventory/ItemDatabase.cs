using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {
	private ReadJSON reader;
	
	public Resource[] resources;
	public Rubbish[] rubbish;
	// Use this for initialization
	void Start () {
		reader = FindObjectOfType<ReadJSON>();

		resources = reader.GetResourceList();
		rubbish = reader.GetRubbishList();

		Debug.Log(resources[0].resourceName);
		Debug.Log(rubbish[0].rubbishName);
	}


}
