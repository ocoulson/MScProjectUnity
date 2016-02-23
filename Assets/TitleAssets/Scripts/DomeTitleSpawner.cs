using UnityEngine;
using System.Collections;

public class DomeTitleSpawner : MonoBehaviour {
	float timer= 5f;
	bool done = false;
	public GameObject dome;
	public GameObject title;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer -= Time.deltaTime;

		if (timer <= 0 && !done) {
			Vector3 domePos = new Vector3(20, 35, transform.position.z);
			Vector3 titlePos = new Vector3(20, 50, transform.position.z);
			GameObject myDome = Instantiate(dome, domePos, Quaternion.identity) as GameObject;
			GameObject myTitle = Instantiate(title, titlePos, Quaternion.identity) as GameObject;

			myDome.transform.parent = this.transform;
			myTitle.transform.parent = this.transform;
			done = true;
		}
	}
}
