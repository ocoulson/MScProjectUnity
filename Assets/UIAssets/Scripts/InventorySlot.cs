using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler {
	private InventoryItem slotItem;
	public InventoryItem SlotItem {
		get {return slotItem;}
	}

	public Image background;
	public Image slotImage;
	public Image border;

	public Sprite emptySlotImage {get; set;}


	public bool IsEmpty { get { return slotItem == null;}}
	// Use this for initialization
	void Start () {
		emptySlotImage = Resources.Load<Sprite>("InventoryItems/EmptyIcon");
	}

	public virtual void PutItemInSlot (InventoryItem item)
	{
		slotItem = item;

		ChangeSprite(slotItem.Sprite);
	}

	public virtual InventoryItem RemoveItemFromSlot() {
		InventoryItem output = slotItem;

		slotItem = null;

		ChangeSprite(emptySlotImage);
		return output;
	}
	public virtual void ChangeSprite (Sprite newSprite)
	{
		slotImage.sprite = newSprite;
	}

	public void SetSize(float width, float height) {
		RectTransform borderTransform = border.GetComponent<RectTransform>();
		RectTransform slotImageTransform = slotImage.GetComponent<RectTransform>();
		RectTransform backgroundTransform = background.GetComponent<RectTransform>();
		RectTransform thisTransfrom = GetComponent<RectTransform>();

		thisTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		thisTransfrom.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

		borderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		borderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

		slotImageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width-4);
		slotImageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height-4);

		backgroundTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width-4);
		backgroundTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height-4);

		slotImageTransform.localPosition = borderTransform.localPosition + new Vector3(2f,-2f);
		backgroundTransform.localPosition = slotImageTransform.localPosition;
	}

	public virtual void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right) {
			if (!IsEmpty) {
				InventoryItem item = RemoveItemFromSlot ();
				PlayerView player = FindObjectOfType<PlayerView> ();
				player.DropItem (item);
			}
		}
	}
}
