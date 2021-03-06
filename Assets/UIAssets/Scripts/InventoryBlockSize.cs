﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryBlockSize : MonoBehaviour {
	public Image background;
	public Image border;

	public void SetSize(float width, float height) {
		RectTransform mainTransform = GetComponent<RectTransform>();
		RectTransform borderTransform = border.GetComponent<RectTransform>();
		RectTransform backgroundTransform = background.GetComponent<RectTransform>();

		mainTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		mainTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

		borderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		borderTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

		backgroundTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width-4);
		backgroundTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height-4);

		backgroundTransform.localPosition = borderTransform.localPosition + new Vector3(2f,-2f);
	}

}
