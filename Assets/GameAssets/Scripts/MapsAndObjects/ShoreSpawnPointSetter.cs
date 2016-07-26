using UnityEngine;
using System.Collections;
using System;

public class ShoreSpawnPointSetter : MonoBehaviour {
	public float firstX;
	public float firstY;
	public GameObject prefab;
	public int quantity;
	public string SpawnPointTag;
	public float deleteStartX;
	public float deleteStopX;


	void Awake ()
	{
		prefab.tag = SpawnPointTag;
		float x = firstX;
		float y = firstY;
		for (int i = 0; i <= quantity; i++) {
			if (x < deleteStartX || x > deleteStopX) {
				GameObject go = Instantiate(prefab, new Vector2(x,y), Quaternion.identity) as GameObject;
				go.transform.SetParent(transform);
			}

			x += 0.16f;
		}
	}
	

}
