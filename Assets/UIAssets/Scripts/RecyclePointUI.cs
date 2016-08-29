using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using ObserverPattern;

public class RecyclePointUI : MonoBehaviour, Observer {
	private RecyclePoint currentPoint;

	private ItemDatabase itemDatabase;


	public int numberOfSlots;
	public int numberOfRows;

	public int Capacity {
		get {
			if (currentPoint != null) {
				return currentPoint.capacity; 
			} else {
				return Int32.MaxValue;
			} 
		}
	}

	public float paddingLeft, paddingTop; 

	public float slotSize;
	public GameObject slotPrefab;
	public GameObject slotHolder;
	public GameObject mainBlock;
	public Button depositButton;
	public Image fullImage;

	public bool IsFull {
		get { return GetCurrentQuantity() >= Capacity;} 
	}
	public Text capacityText;
	public Text currentQuantityText;

	public List<GameObject> slotsObjects {get; private set;}
	public List<DropOffPointSlot> slots {get; private set;}

	// Use this for initialization
	void Start () {
		itemDatabase = FindObjectOfType<ItemDatabase>();
		slotsObjects = new List<GameObject>();
		slots = new List<DropOffPointSlot>();
		SetupRecyclePointUI();
		capacityText.text = Capacity.ToString();
		currentQuantityText.text = "0";
		fullImage.gameObject.SetActive(false);
	}

	void Update ()
	{
		if (IsFull) {
			DisableDepositButton ();
		} else {
			EnableDepositButton();
		}
	}

	public void DisableDepositButton ()
	{
		depositButton.interactable = false;
		fullImage.gameObject.SetActive(true);
	}
	public void EnableDepositButton ()
	{
		depositButton.interactable = true;
		fullImage.gameObject.SetActive(false);
	}

	public void AddItem (InventoryItem rubbishItem)
	{
		DropOffPointSlot[] slotArray = slots.ToArray();

		DropOffPointSlot destinationSlot = Array.Find<DropOffPointSlot>(slotArray, slot => slot.SlotItem.ItemName == rubbishItem.ItemName);

		destinationSlot.PutItemInSlot(rubbishItem);
		currentQuantityText.text = GetCurrentQuantity().ToString();

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
					dropOffSlot.SlotItem = allRubbishItems.Pop ();
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

	public void ShowUI (RecyclePoint point)
	{
		currentPoint = point;
		foreach (InventoryItem item in currentPoint.RecyclingItems) {
			AddItem(item);
		}
		mainBlock.SetActive(true);
		slotHolder.SetActive(true);
	}
	public void HideUI() {
		
		mainBlock.SetActive(false);
		slotHolder.SetActive(false);
		ClearSlots();
		currentPoint = null;
	}

	public int GetCurrentQuantity ()
	{
		int total = 0;
		foreach (DropOffPointSlot slot in slots) {
			total += slot.slotQuantity;
		}
		return total;
	}

	private void ClearSlots ()
	{
		foreach (DropOffPointSlot slot in slots) {
			slot.EmptySlot();
		}
	}




	#region Observer implementation
	public void OnNotify ()
	{
		if (currentPoint != null) {
			ClearSlots();
			foreach (InventoryItem item in currentPoint.RecyclingItems) {
				AddItem(item);
			}
		}
	}
	#endregion
}
