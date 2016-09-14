using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class RecyclingReceiverFeedbackUI : MonoBehaviour {

	public Image contentsImage;
	public Text contentsText;

	public GameObject inventoryBlock;
	public RecyclingReceiver receiver;

	private List<GameObject> slots;
	public GameObject slotPrefab;
	public GameObject slotHolder;
	public float slotSize, paddingLeft, paddingTop;

	private float blockWidth;
	private float blockHeight;

	public GameObject tooltipObject;
	private static GameObject tooltip;

	public Text sizeTextObject;
	private static Text sizeText;

	public Text visualTextObject;
	private static Text visualText;

	void Start ()
	{
		tooltip = tooltipObject;
		sizeText = sizeTextObject;
		visualText = visualTextObject;

	}

	public void SetText (string newText)
	{
		contentsText.text = newText;
	} 

	public void SetImage (Sprite newSprite)
	{
		contentsImage.sprite = newSprite;
	}

	public void SetupUI ()
	{
		SetText (receiver.intendedContents);
		SetImage (receiver.contentIcon);
		RectTransform inventoryRect = inventoryBlock.GetComponent<RectTransform> ();
		slots = new List<GameObject> ();

		int NumberOfSlots = receiver.DepositedItems.Count;
		if (NumberOfSlots < 16)
			NumberOfSlots = 16;
		int columns = 4;
		int rows = 0;
		if (NumberOfSlots % columns == 0) { 
			rows = (int)(NumberOfSlots / columns);
		} else {
			rows = ((int)(NumberOfSlots / columns)) +1;
		}
		blockWidth = (columns * (slotSize + paddingLeft)) + paddingLeft;
		blockHeight = (rows * (slotSize + paddingTop)) + paddingTop + (slotSize/2);
		inventoryBlock.GetComponent<InventoryBlockSize> ().SetSize (blockWidth, blockHeight);

		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < columns; x++) {
				 GameObject newSlot = Instantiate(slotPrefab) as GameObject;
				 newSlot.GetComponent<InventorySlot>().SetSize(slotSize, slotSize);
				 newSlot.name = "Slot";
				 newSlot.transform.SetParent(slotHolder.transform);

				 RectTransform slotRect = newSlot.GetComponent<RectTransform>();
				 slotRect.localPosition = inventoryRect.localPosition + new Vector3(paddingLeft * (x+1) + (slotSize * x),
				 																	 -paddingTop * (y+1) - (slotSize * y) - (slotSize/2)); 

				 slots.Add(newSlot);
			}
		}
	}

	public void PopulateSlots ()
	{
		for (int i = 0; i < receiver.DepositedItems.Count; i++) {
			InventorySlot currentSlot = slots [i].GetComponent<InventorySlot> ();
			InventoryItem currentItem = receiver.DepositedItems [i];

			//Display a red colour if the item shouldn't be in this receiver.
			if (!currentItem.ContainedResources.Contains (receiver.intendedContents)) {
				currentSlot.background.color = new Color32(150,50,50,100);
			}
			currentSlot.PutItemInSlot(currentItem);

		}
	}
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


}
