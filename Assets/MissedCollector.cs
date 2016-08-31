using UnityEngine;
using System.Collections;

public class MissedCollector : MonoBehaviour {

	private SortingGameScorer scorer;
	// Use this for initialization
	void Start () {
		scorer = FindObjectOfType<SortingGameScorer>();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		scorer.MissedItem();
		Destroy(col.gameObject);
	}

}
