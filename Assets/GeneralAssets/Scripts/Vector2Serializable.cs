using UnityEngine;
using System.Collections;

[System.Serializable]
public class Vector2Serializable {

	public float x;
	public float y;

	public Vector2Serializable (Vector2 vector)
	{
		Apply(vector);
	}
	public void Apply (Vector2 vector)
	{
		this.x = vector.x;
		this.y = vector.y;
	}

	public Vector2 Vector2 { 
		get { return new Vector2 (x, y); } 
		set { Apply (value); } }
}
