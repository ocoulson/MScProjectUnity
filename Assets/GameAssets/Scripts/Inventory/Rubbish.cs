using UnityEngine;
using System.Collections.Generic;

public class Rubbish : InventoryItem
{
	public int rubbishId;
	public string rubbishName;
	public string rubbishDescription;
	public List<Resource> containedResources;


	public Rubbish ()
	{
		containedResources = new List<Resource>();
	}



}


