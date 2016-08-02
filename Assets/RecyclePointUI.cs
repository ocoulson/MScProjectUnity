using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecyclePointUI : MonoBehaviour {
	private ItemDatabase itemDatabase;

	public int numberOfSlots;
	public int numberOfRows;

	public float paddingLeft, paddingTop; 

	public float slotSize;
	public GameObject slotPrefab;
	public GameObject slotHolder;
	public GameObject mainBlock;

	public List<GameObject> slots {get; private set;}

	// Use this for initialization
	void Start () {
		itemDatabase = FindObjectOfType<ItemDatabase>();
		slots = new List<GameObject>();
		SetupRecyclePointUI();
	}

	public void AddRubbishItem (InventoryItem rubbishItem)
	{
		foreach (GameObject slot in slots) {
			DropOffPointSlot dropOffSlot = slot.GetComponent<DropOffPointSlot> ();
			if (dropOffSlot.slotItem.itemName == rubbishItem.itemName) {
				dropOffSlot.PutItemInSlot(rubbishItem);
			}
		}
	}

	//TODO: Edit if more rubbish items are added to the DB
	private void SetupRecyclePointUI ()
	{
		numberOfSlots = itemDatabase.rubbish.Length;
		Stack<InventoryItem> allRubbishItems = itemDatabase.GetAllRubbishItems ();

		int numberOfColumns = numberOfSlots / numberOfRows;
		if (numberOfSlots % numberOfRows != 0) {
			numberOfColumns++;
		}

		int count = numberOfSlots;
		for (int y = 0; y < numberOfRows; y++) {
			for (int x = 0; x < numberOfColumns; x++) {
				if (count > 0) {
					GameObject newSlot = Instantiate (slotPrefab) as GameObject;
					newSlot.name = "Slot";
					newSlot.transform.SetParent (slotHolder.transform);

					DropOffPointSlot slot = newSlot.GetComponent<DropOffPointSlot> ();
					slot.SetSize (slotSize, slotSize);
					slot.slotItem = allRubbishItems.Pop ();
					slot.SetSlotImage ();

					RectTransform slotRect = newSlot.GetComponent<RectTransform> ();
					RectTransform holderRect = slotHolder.GetComponent<RectTransform> ();
					float xDivision = (holderRect.sizeDelta.x / numberOfColumns);
					slotRect.localPosition = new Vector3 ((xDivision * x), -(slotSize * y)); 

					slots.Add (newSlot);
					count --;
				}
			}
		}
	}

	public void ShowUI() {
		mainBlock.SetActive(true);
		slotHolder.SetActive(true);
	}
	public void HideUI() {
		mainBlock.SetActive(false);
		slotHolder.SetActive(false);
	}
}
