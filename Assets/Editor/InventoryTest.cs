using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

namespace tests {

	[TestFixture]
	public class InventoryTest {

		private InventoryItem[] rubbishItems;
		private Inventory emptyInventory;
		private Inventory fullInventory;

		[SetUp]
		public void Init ()
		{
			emptyInventory = new Inventory (10);

			JsonDataHandler json = new JsonDataHandler ();
			rubbishItems = json.GetInventoryItemArray ("Rubbish");

			fullInventory = new Inventory (rubbishItems.Length);

			foreach (InventoryItem item in rubbishItems) {
				fullInventory.AddItem(item);
			}

		}

		[Test]
		public void TestEmptyInventoryIsEmpty() {
			Assert.That(emptyInventory.IsEmpty);
			Assert.That(emptyInventory.Items.Count == 0);
		}

		[Test]
		public void TestFullInventoryIsFull ()
		{
			Assert.That(fullInventory.IsFull);
		}

		[Test]
		public void TestInventoryIncreaseCapacityToHigherCapacity() {
			int beforeSize = emptyInventory.Size;
			emptyInventory.IncreaseCapacity(beforeSize + 10);

			Assert.That(emptyInventory.Size == beforeSize + 10);
		}

		[Test]
		[ExpectedException(typeof (UnityException))]
		public void TestInventoryIncreaseCapacityToLowerCapacity() {
			emptyInventory.IncreaseCapacity(emptyInventory.Size - 5);
		}

		[Test]
		public void TestAddItemToEmptyInventory ()
		{
			InventoryItem newItem = new InventoryItem();
			emptyInventory.AddItem(newItem);
			Assert.That(emptyInventory.Items.Count == 1);
			Assert.That(emptyInventory.Items[0] == newItem);
		}

		[Test]
		[ExpectedException(typeof (UnityException))]
		public void TestRemoveItemFromEmptyInventory() {
			emptyInventory.RemoveItem(new InventoryItem());
		}

		[Test]
		[ExpectedException (typeof (UnityException))]
		public void TestAddItemToFullInventory() {
			fullInventory.AddItem(new InventoryItem());
		}

		[Test]
		public void TestRemoveItemFromFullInventory() {
			InventoryItem toBeRemoved = fullInventory.Items[0];
			InventoryItem removed = fullInventory.RemoveItem(fullInventory.Items[0]);

			Assert.That (toBeRemoved == removed);
			Assert.That (!fullInventory.IsFull);
			Assert.That (fullInventory.Count == (fullInventory.Size -1));
			Assert.That (!fullInventory.Contains(removed));
		}

		[Test]
		public void TestRemoveAllItemsFromFullInventory() {
			fullInventory.RemoveAll();
			Assert.That(fullInventory.IsEmpty);

		}

		[Test]
		[ExpectedException (typeof (UnityException))]
		public void TestRemoveAllItemsFromEmptyInventory() {
			emptyInventory.RemoveAll();
		}

		[Test]
		[ExpectedException (typeof (UnityException))]
		public void TestCreateInventoryWithInitialSizeLessThanZero() {
			new Inventory(-5);
		}

		[Test]
		public void TestToString() {
			InventoryItem item1 = new InventoryItem();
			InventoryItem item2 = new InventoryItem();
			InventoryItem item3 = new InventoryItem();

			item1.ItemName = "Item1";
			item2.ItemName = "Item2";
			item3.ItemName = "Item3";

			emptyInventory.AddItem(item1);
			emptyInventory.AddItem(item2);
			emptyInventory.AddItem(item3);

			string expected = "Item1, Item2, Item3";
			Assert.That(emptyInventory.ToString() == expected);

			InventoryItem item4 = new InventoryItem();
			item4.ItemName = "Item4";

			emptyInventory.AddItem(item4);
			expected = "Item1, Item2, Item3, Item4";
			Assert.That(emptyInventory.ToString() == expected);
		}
	}
	

}