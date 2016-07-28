using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour {

	private float iWidth;
	private float iHeight;
	public GameObject inventoryBlock;
	public GameObject slotHolder;
	public Text titleText;
	public int numberOfSlots;
	public int numberOfRows;

	public float paddingLeft, paddingTop; 

	public float slotSize;
	public GameObject slotPrefab;


	private List<GameObject> slots;

	// Use this for initialization
	void Start () {
		SetupLayout();

	}
	
	// Update is called once per frame
	void Update () {
	
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
				 newSlot.GetComponent<InventoryBlockSize>().SetSize(slotSize, slotSize);
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
