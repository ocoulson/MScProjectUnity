using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ObserverPattern;

public class InventoryUIManager : MonoBehaviour, Observer {

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

	public GameObject tooltipObject;
	private static GameObject tooltip;

	public Text sizeTextObject;
	private static Text sizeText;

	public Text visualTextObject;
	private static Text visualText;

	#region Observer implementation

	public void OnNotify ()
	{
		UpdateInventoryUi();
	}

	#endregion

	void Start ()
	{
		tooltip = tooltipObject;
		sizeText = sizeTextObject;
		visualText = visualTextObject;

	}


	//ShowTooltip and HideTooltip are used by the event triggers on the InventorySlot prefab for mouse enter and exit to show / hide the tooltip
	public void ShowTooltip (GameObject inventorySlot)
	{
		InventorySlot slot = inventorySlot.GetComponent<InventorySlot> ();

		if (!slot.IsEmpty) {
			tooltip.SetActive(true);
			float xPosition = inventorySlot.transform.position.x + paddingLeft;
			float yPosition = inventorySlot.transform.position.y - slotSize - paddingTop;
			tooltip.transform.position = new Vector2(xPosition,yPosition);

			string tooltipText = slot.SlotItem.GetTooltipInfo("lime", 6);

			sizeText.text = tooltipText;
			visualText.text = tooltipText;


		}
		

	}
	public void HideTooltip ()
	{
		tooltip.SetActive(false);
	}

	private void UpdateInventoryUi ()
	{
		for (int i = 0; i < playerInventory.Size; i++) {
			InventorySlot currentSlot = slots [i].GetComponent<InventorySlot> ();
			if (i < playerInventory.Items.Count){
				currentSlot.PutItemInSlot (playerInventory.Items [i]);
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
		inventory.AddObserver(this);

		numberOfSlots = playerInventory.Size;

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
	public void ShowUI() {
		inventoryBlock.SetActive(true);
		slotHolder.SetActive(true);
	}
	public void HideUI() {
		inventoryBlock.SetActive(false);
		slotHolder.SetActive(false);
	}
}
