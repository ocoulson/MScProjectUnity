using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	public GameObject[] clouds;
	// Use this for initialization

	float minX;
	float maxX;
	float minY;
	float maxY;

	float outerSpawnX;
	void Start ()
	{
		//Get scene limits from camera and store as max min values.
		float zDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,zDistance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,zDistance));

		Vector3 topMost = Camera.main.ViewportToWorldPoint(new Vector3(0,1, zDistance));
		Vector3 botMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0.5f, zDistance));

		minX = leftMost.x;
		maxX = rightMost.x;
		minY = botMost.y;
		maxY = topMost.y;

		outerSpawnX = rightMost.x + 5f;

		for (int i = 0; i < 10; i++) {
			spawnCloud(false);
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
		int random = Random.Range (1, 100);

		if (random > 98) {
			spawnCloud(true);
		}
	}

	void spawnCloud (bool outer)
	{
		float cloudScale = Random.Range (0.6f, 1f);
		int cloudId = Random.Range (0, 2);
		float cloudY = Random.Range(minY, maxY);
		float cloudX;

		if (outer) {
			cloudX = outerSpawnX;
		} else {
			cloudX = Random.Range(minX, maxX);
		}
			
		Vector3 pos = new Vector3(cloudX, cloudY, transform.position.z);

		GameObject cloud = Instantiate(clouds[cloudId], pos, Quaternion.identity) as GameObject;
		cloud.transform.localScale = new Vector3(cloudScale, cloudScale, 0);
		cloud.transform.parent = this.transform;
	}

	public float getMinX ()
	{
		return minX;
	}
}
