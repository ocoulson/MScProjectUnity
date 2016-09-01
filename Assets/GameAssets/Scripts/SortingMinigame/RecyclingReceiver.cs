using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RecyclingReceiver : MonoBehaviour {

	public string intendedContents;
	private List<InventoryItem> depositedItems;
	private SortingGameScorer scorer;

	void Start ()
	{
		depositedItems = new List<InventoryItem>();
		scorer = FindObjectOfType<SortingGameScorer>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		InventoryItem item = col.GetComponent<Collectable>().item;
		depositedItems.Add(item);
		Destroy(col.gameObject);
		Score(item);
	}

	private void Score (InventoryItem item)
	{
		if (item.ContainedResources.Contains (intendedContents)) {
			scorer.CorrectBox ();
		} else {
			scorer.IncorrectBox();
		}
	}
}
