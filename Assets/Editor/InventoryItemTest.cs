using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;

namespace tests {
	[TestFixture]
	public class InventoryItemTest {

		private InventoryItem emptyItem;
		private InventoryItem glassBottle;


		[SetUp]
		public void Init() {
			emptyItem = new InventoryItem();

			glassBottle = new InventoryItem();
			glassBottle.ItemId = 10;
			glassBottle.ItemName = "GlassBottle1";
			glassBottle.ItemDescription = "A small drinks bottle made of glass";
			glassBottle.ItemType = ItemType.RUBBISH;
			glassBottle.ContainedResources.Add("Glass");
			glassBottle.ContainedResources.Add("Glass");
			glassBottle.ContainedResources.Add("Paper");
		}

		[Test]
		public void TestEmptyInventoryItem() {
			Assert.That(emptyItem.ItemId == 0);
			Assert.IsNull(emptyItem.ItemName);
			Assert.IsNull(emptyItem.ItemDescription);

			Assert.IsNotNull(emptyItem.ContainedResources);
			Assert.That(emptyItem.ContainedResources.Count == 0);

			Assert.IsNull(emptyItem.Sprite);
		}

		[Test]
		public void TestItemName() {
			Assert.That(glassBottle.ItemName == "GlassBottle1");
		}

		[Test]
		public void TestItemDescription() {
			Assert.That(glassBottle.ItemDescription == "A small drinks bottle made of glass");
		}
		[Test]
		public void TestItemId() {
			Assert.That(glassBottle.ItemId == 10);
		}
		[Test]
		public void TestItemType() {
			Assert.That(glassBottle.ItemType == ItemType.RUBBISH);
		}
		[Test]
		public void TestContainedResources ()
		{
			Assert.That(glassBottle.ContainedResources.Count == 3);
			Assert.That(glassBottle.ContainedResources[0] == "Glass");
			Assert.That(glassBottle.ContainedResources[1] == "Glass");
			Assert.That(glassBottle.ContainedResources[2] == "Paper");
		}

		[Test]
		public void TestSpriteBeforeInitialise() {
			Assert.IsNull(glassBottle.Sprite);
		}

		[Test]
		public void TestInitialiseItem(){
			glassBottle.InitialiseItem();
			Assert.IsNotNull(glassBottle.Sprite);
			Assert.That(glassBottle.Sprite.name == "GlassBottle1Icon");
		}

		[Test]
		public void TestGetNameFormatted() {
			Assert.That(glassBottle.GetNameFormatted() == "Glass Bottle");
		}

		[Test]
		public void TestGetToolTipInfo_RedTitle_8WordsPerLine() {
			string expected = "<color=red><size=16>Glass Bottle</size></color>\n<i>A small drinks bottle made of glass</i>\n<color=red>Recyclable Resources:</color>\n<i>Glass\nPaper\n</i>";
			Assert.That(glassBottle.GetTooltipInfo("red", 8) == expected);
		}

		[Test]
		public void TestGetToolTipInfo_GreenTitle_6WordsPerLine() {
			string expected = "<color=lime><size=16>Glass Bottle</size></color>\n<i>A small drinks bottle made of \nglass </i>\n<color=red>Recyclable Resources:</color>\n<i>Glass\nPaper\n</i>";
			Assert.That(glassBottle.GetTooltipInfo("lime", 6) == expected);
		}
	}

}
