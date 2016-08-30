using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropOffPointSlot : InventorySlot {

	private int quantity;
	public Text quantityText;

	public int slotQuantity {
		get {return quantity;}
	}
	// Use this for initialization
	void Start () {
		quantity = 0;
		emptySlotImage = Resources.Load<Sprite>("InventoryItems/EmptyIcon");
	}

	public void PutItemInSlot ()
	{
		quantity ++;
		ChangeQuantityText();
	}

	public void RemoveItemFromSlot() {
		quantity --;
		ChangeQuantityText();

	}

	public override void OnPointerClick (PointerEventData eventData)
	{
		Debug.Log("Do nothing");
	}
	private void ChangeQuantityText() {
		quantityText.text = quantity.ToString();
	}
	public void SetSlotImage() {
		slotImage.sprite = SlotItem.Sprite;
	}

	public void EmptySlot() {
		quantity = 0;
	}
}
