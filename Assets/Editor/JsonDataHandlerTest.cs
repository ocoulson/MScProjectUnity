using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using LitJson;
using DialogueBlocks;

namespace tests {

	[TestFixture]
	public class JsonDataHandlerTest {

		private JsonDataHandler handler;

		[SetUp]
		public void Init ()
		{
				
			handler = new JsonDataHandler();
		}	

		[Test]
		public void TestDialogueJsonDataLoaded() {
			Assert.IsNotNull(handler.DialogueJsonData);
		}

		[Test]
		public void TestItemsJsonDataLoaded() {
			Assert.IsNotNull(handler.ItemsJsonData);
		}


		[Test]
		[ExpectedException(typeof (KeyNotFoundException))]
		public void TestGetUnknownCharacterDialogue() {
			DialogueBlock[] noCharacterDialogue = handler.GetCharacterDialogue("boo");
			noCharacterDialogue.ToString();
		}

		[Test]
		public void TestGetKnownCharacterDialogue() {
			DialogueBlock[] mayorDialogue = handler.GetCharacterDialogue("Mayor");
			Assert.IsNotNull(mayorDialogue);
		}

		[Test]
		public void TestGetRubbishItems() {
			InventoryItem[] items = handler.GetInventoryItemArray("Rubbish");
			Assert.IsNotNull(items);
		}

		[Test]
		public void TestRubbishItemsLength() {
			InventoryItem[] rubbishItems = handler.GetInventoryItemArray("Rubbish");

			Assert.That(rubbishItems.Length == 17);
		} 

		[Test]
		public void TestGetResourceItems() {
			InventoryItem[] resourceItems = handler.GetInventoryItemArray("Resources");
			Assert.IsNotNull(resourceItems);
		}

		[Test]
		public void TestResourceItemsLength() {
			InventoryItem[] resourceItems = handler.GetInventoryItemArray("Resources");
			Assert.That(resourceItems.Length == 5);
		} 

	}
}