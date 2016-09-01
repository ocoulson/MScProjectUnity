using UnityEngine;
using System.Collections;

public class MissedCollector : MonoBehaviour {

	private SortingGameSpawner spawner;
	private SortingGameScorer scorer;
	// Use this for initialization
	void Start () {
		spawner = FindObjectOfType<SortingGameSpawner>();
		scorer = FindObjectOfType<SortingGameScorer>();
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		scorer.MissedItem();
		spawner.ReturnMissedRubbish(col.GetComponent<Collectable>().item);
		Destroy(col.gameObject);
	}

}
