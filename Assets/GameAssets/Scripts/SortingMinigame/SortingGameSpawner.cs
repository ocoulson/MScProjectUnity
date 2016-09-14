using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortingGameSpawner : MonoBehaviour {
	public float frequency;
	private ItemDatabase itemDB;
	private float timer;
	private List<InventoryItem> toBeSpawned;
	private GameManager gameManager;
	public Canvas feedback;

	public int StartCount;
	public int Recycled;
	public int Remaining;

	public bool partOfGame;
	private bool GameFinished;


	void Start ()
	{
		gameManager = FindObjectOfType<GameManager> ();
		itemDB = FindObjectOfType<ItemDatabase> ();
		timer = 0;

		if (gameManager.CurrentGame == null || gameManager.CurrentGame.RecyclePoints [0].IsEmpty) {
			toBeSpawned = GetRandomRubbish (30);
			partOfGame = false;
		} else {
			toBeSpawned = gameManager.CurrentGame.RecyclePoints[0].RecyclingItems;
			gameManager.CurrentGame.RecyclePoints[0].RecyclingItems = new List<InventoryItem>();
			partOfGame = true;
		}
		StartCount = toBeSpawned.Count;
		Remaining = StartCount;
		Recycled = 0;

		GameFinished = false;
	}

	private List<InventoryItem> GetRandomRubbish(int quantity) {
		List<InventoryItem> output = new List<InventoryItem>();
		for (int i = 0; i < quantity; i++) {
			output.Add(itemDB.rubbish[Random.Range(0, itemDB.rubbish.Length)].GetCopy());
		}
		return output;
	}
	// Update is called once per frame
	void Update ()
	{
		if (GameFinished) {
			return;
		}
		timer += Time.deltaTime;
		//Debug.Log(timer);
		if (timer > frequency && toBeSpawned.Count > 0) {
			SpawnRubbish ();
			timer = 0;
		}

		//TODO: Load a score screen giving feedback on results before returning to the main menu or the game
		if (Remaining == 0 && Recycled == StartCount) {
			feedback.gameObject.SetActive(true);
			FindObjectOfType<SortingGameFeedback>().DisplayFeedback(partOfGame);
			GameFinished = true;
		}
	}
	private void SpawnRubbish() {

		GameObject newRubbishItem = itemDB.CreateItemGameObject(toBeSpawned[0]);
		toBeSpawned.Remove(toBeSpawned[0]);
		Debug.Log(newRubbishItem.GetComponent<Collectable>().item.ItemName);

		newRubbishItem.transform.position = transform.position;
		newRubbishItem.transform.SetParent(transform);
		Vector3 scale = newRubbishItem.transform.localScale;
		newRubbishItem.transform.localScale = scale * 10;

		newRubbishItem.AddComponent<Rigidbody2D>();

		Destroy(newRubbishItem.GetComponent<Collider2D>());
		newRubbishItem.AddComponent<PolygonCollider2D>();
		Remaining --;
	}
	public void ReturnMissedRubbish(InventoryItem missed) {
		toBeSpawned.Add(missed);
		Remaining ++;
	}
}
