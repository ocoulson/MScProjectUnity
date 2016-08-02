using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUIManager : MonoBehaviour {

	private float iWidth;
	private float iHeight;
	public GameObject inventoryBlock;
	public GameObject slotHolder;
	public Text titleText;

	private int numberOfSlots;
	public int numberOfRows;

	public float paddingLeft, paddingTop; 

	public float slotSize;
	public GameObject slotPrefab;
	private List<GameObject> slots;

	public Inventory playerInventory;

	public void UpdateInventoryUi ()
	{
		for (int i = 0; i < playerInventory.size; i++) {
			InventorySlot currentSlot = slots [i].GetComponent<InventorySlot> ();
			if (i < playerInventory.items.Count){
				currentSlot.PutItemInSlot (playerInventory.items [i]);
			} else {
				if (!currentSlot.IsEmpty) {
					currentSlot.RemoveItemFromSlot();
				}
			}
		}
	}

	public void LinkInventoryToUI (Inventory inventory)
	{

		playerInventory = inventory;
		numberOfSlots = playerInventory.size;

		SetupLayout();
	}

	private void SetupLayout ()
	{
		
		RectTransform inventoryRect = inventoryBlock.GetComponent<RectTransform>();
		slots = new List<GameObject> ();
		int numberOfColumns = numberOfSlots / numberOfRows;
		iWidth = (numberOfColumns * (slotSize + paddingLeft)) + paddingLeft;
		iHeight = (numberOfRows + 1) * (slotSize + paddingTop) + paddingTop;

		titleText.rectTransform.position = inventoryRect.position + new Vector3 (iWidth/2, -iHeight/(numberOfRows*2));

		inventoryBlock.GetComponent<InventoryBlockSize> ().SetSize (iWidth, iHeight);


		for (int y = 1; y < numberOfRows+1; y++) {
			for (int x = 0; x < numberOfColumns; x++) {
				 GameObject newSlot = Instantiate(slotPrefab) as GameObject;
				 newSlot.GetComponent<InventorySlot>().SetSize(slotSize, slotSize);
				 newSlot.name = "Slot";
				 newSlot.transform.SetParent(slotHolder.transform);

				 RectTransform slotRect = newSlot.GetComponent<RectTransform>();
				 slotRect.localPosition = inventoryRect.localPosition + new Vector3(paddingLeft * (x+1) + (slotSize * x),
				 																	 -paddingTop * (y+1) - (slotSize * y)); 

				 slots.Add(newSlot);
			}
		}
	}

	public void Toggle() {
		inventoryBlock.SetActive(!inventoryBlock.activeInHierarchy);
		slotHolder.SetActive(!slotHolder.activeInHierarchy);
	}

}
