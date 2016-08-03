using UnityEngine;
using System;
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

	public List<GameObject> slotsObjects {get; private set;}
	public List<DropOffPointSlot> slots {get; private set;}

	// Use this for initialization
	void Start () {
		itemDatabase = FindObjectOfType<ItemDatabase>();
		slotsObjects = new List<GameObject>();
		slots = new List<DropOffPointSlot>();
		SetupRecyclePointUI();
	}

	public void AddRubbishItem (InventoryItem rubbishItem)
	{
		DropOffPointSlot[] slotArray = slots.ToArray();

		DropOffPointSlot destinationSlot = Array.Find<DropOffPointSlot>(slotArray, slot => slot.slotItem.itemName == rubbishItem.itemName);

		destinationSlot.PutItemInSlot(rubbishItem);
//		Debug.Log("Adding " + rubbishItem.GetNameFormatted());
//		foreach (GameObject slot in slots) {
//			DropOffPointSlot dropOffSlot = slot.GetComponent<DropOffPointSlot> ();
//			if (dropOffSlot.slotItem.itemName == rubbishItem.itemName) {
//				dropOffSlot.PutItemInSlot(rubbishItem);
//			}
//		}
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
					GameObject newSlotObject = Instantiate (slotPrefab) as GameObject;
					newSlotObject.name = "Slot";
					newSlotObject.transform.SetParent (slotHolder.transform);

					DropOffPointSlot dropOffSlot = newSlotObject.GetComponent<DropOffPointSlot> ();
					dropOffSlot.SetSize (slotSize, slotSize);
					dropOffSlot.slotItem = allRubbishItems.Pop ();
					dropOffSlot.SetSlotImage ();

					RectTransform slotRect = newSlotObject.GetComponent<RectTransform> ();
					RectTransform holderRect = slotHolder.GetComponent<RectTransform> ();
					float xDivision = (holderRect.sizeDelta.x / numberOfColumns);
					slotRect.localPosition = new Vector3 ((xDivision * x), -(slotSize * y)); 

					slotsObjects.Add (newSlotObject);
					slots.Add(dropOffSlot);
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

	public int TotalContentsQuantity ()
	{
		int total = 0;
		foreach (DropOffPointSlot slot in slots) {
			total += slot.slotQuantity;
		}
		return total;
	}
}
