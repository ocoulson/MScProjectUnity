using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RecyclingReceiver : MonoBehaviour {

	public string intendedContents;
	public Sprite contentIcon;
	private List<InventoryItem> depositedItems;

	public List<InventoryItem> DepositedItems {
		get {return depositedItems;}
	}

	private SortingGameScorer scorer;
	private SortingGameSpawner spawner;

	void Start ()
	{
		depositedItems = new List<InventoryItem>();
		scorer = FindObjectOfType<SortingGameScorer>();
		spawner = FindObjectOfType<SortingGameSpawner>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		InventoryItem item = col.GetComponent<Collectable>().item;
		depositedItems.Add(item);
		Destroy(col.gameObject);
		Score(item);
		spawner.Recycled++;
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
