using UnityEngine;
using UnityEditor;
using NUnit.Framework;

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
		public void TestEmptyInventoryIncreaseCapacityToHigherCapacity() {
			int beforeSize = emptyInventory.Size;
			emptyInventory.IncreaseCapacity(beforeSize + 10);

			Assert.That(emptyInventory.Size == beforeSize + 10);
		}

		[Test]
		[ExpectedException(typeof (UnityException))]
		public void TestEmptyInventoryIncreaseCapacityToLowerCapacity() {
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
		}
	}
	

}