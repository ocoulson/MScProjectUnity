using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[Serializable]
public class CheckPointList {

	private List<CheckPoint> baseList;

	public CP_STATUS this [string nameKey] {
		get { 
			CheckPoint target = Array.Find (baseList.ToArray (), cp => cp.Name == nameKey);

			if (target != null) {
				return target.Status;
			} else {
				throw new KeyNotFoundException ("Key '" + nameKey + "' not found in CheckPointList");
			}
		}
		set { 
			CheckPoint target = Array.Find (baseList.ToArray (), cp => cp.Name == nameKey);

			if (target != null) {
				target.Status = value;
			} else {
				throw new KeyNotFoundException ("Key '" + nameKey + "' not found in CheckPointList");
			}
		}
	}
	public CheckPointList() {
		baseList = new List<CheckPoint>();
	}
	public void Add(CheckPoint newCP) {
		baseList.Add(newCP);
	}
	public void Add (string key)
	{
		baseList.Add(new CheckPoint(key));
	}

	public bool Contains (string key)
	{
		return Array.Exists(baseList.ToArray(), cp => cp.Name == key); 
	}
}
