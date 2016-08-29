using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropOffPointSlot : InventorySlot {

	public Stack<InventoryItem> containedItems {get; private set;}
	public Text quantityText;

	public int slotQuantity {
		get { 
			if (containedItems != null) {
				return containedItems.Count;
			} else
				return 0;
		}
	}
	// Use this for initialization
	void Start () {
		containedItems = new Stack<InventoryItem>();
		emptySlotImage = Resources.Load<Sprite>("InventoryItems/EmptyIcon");
	}

	public override void PutItemInSlot (InventoryItem item)
	{
		containedItems.Push(item);
		ChangeQuantityText();
	}

	public override InventoryItem RemoveItemFromSlot() {
		InventoryItem output = containedItems.Pop();
		ChangeQuantityText();

		return output;
	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log("Do nothing");
	}
	private void ChangeQuantityText() {
		quantityText.text = containedItems.Count.ToString();
	}
	public void SetSlotImage() {
		slotImage.sprite = SlotItem.Sprite;
	}

	public void EmptySlot() {
		containedItems = new Stack<InventoryItem>();
	}
}
