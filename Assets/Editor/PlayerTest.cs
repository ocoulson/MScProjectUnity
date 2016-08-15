using UnityEngine;
using UnityEditor;
using NUnit.Framework;

namespace RegularTests {
	[TestFixture]
	public class PlayerTest {

		private Player player1;
		private Player fullInventoryPlayer;

	    [SetUp]
	    public void Init ()
		{
			player1 = new Player ("Player1", Gender.FEMALE, "Eve2", new Vector2 (0, 0));
			fullInventoryPlayer = new Player ("Ollie", Gender.MALE, "Evan1", Vector2.zero);
			fullInventoryPlayer.InitialiseInventory (20);

			for (int i = 0; i < 20; i++) {
				InventoryItem item = new InventoryItem();
				item.ItemName = "Item" + i;
				item.ItemId = i;
				item.ItemType = ItemType.RUBBISH;
				item.ItemDescription = string.Empty;
				fullInventoryPlayer.AddItem(item);
			}
	    }

	    [Test]
	    public void TestConstructor() {
	    	Assert.That(player1.Name == "Player1");
	    	Assert.That(player1.Gender == Gender.FEMALE);
	    	Assert.That(player1.SpriteName == "Eve2");
	    	Assert.That(player1.CurrentPosition == Vector2.zero);
	    	Assert.Null(player1.Inventory);
	    	Assert.That(player1.Tools.Count == 0);
	    } 

	    [Test]
	    public void TestInitialiseInventory() {
	    	Assert.Null(player1.Inventory);
	    	player1.InitialiseInventory(10);
	    	Assert.NotNull(player1.Inventory);
	    	Assert.That(player1.InventoryInitialised);
	    	Assert.That(player1.Inventory.Size == 10);
	    }

	    [Test]
	    [ExpectedException (typeof (UnityException))]
	    public void TestInitialiseExistingInventory() {
	    	player1.InitialiseInventory(10);
	    	Assert.That(player1.InventoryInitialised);
	    	player1.InitialiseInventory(20);
	    }

	    [Test]
	    public void TestIncreaseInventorySize ()
		{
			player1.InitialiseInventory(10);
			Assert.That(player1.InventoryInitialised);
			Assert.That(player1.Inventory.Size == 10);
			player1.IncreaseInventorySize(20);

			Assert.That(player1.Inventory.Size == 20);
		}

		[Test]
	    public void TestIncreaseInventorySizeToLower ()
		{
			player1.InitialiseInventory(10);
			Assert.That(player1.InventoryInitialised);
			Assert.That(player1.Inventory.Size == 10);
			player1.IncreaseInventorySize(5);

			Assert.That(player1.Inventory.Size == 10);
		}

		[Test]
		public void TestFullInventory ()
		{
			Assert.That(fullInventoryPlayer.Inventory.Count == fullInventoryPlayer.Inventory.Size);
		}

		[Test]
	    public void TestAddItemToEmptyInventory ()
		{
			player1.InitialiseInventory(10);
			Assert.That(player1.InventoryInitialised);
			InventoryItem item = new InventoryItem();
			item.ItemName = "Item1";
			player1.AddItem(item);

			Assert.That(player1.Inventory.Count == 1);
			Assert.That(player1.Inventory.Contains(item));

		}

		[Test]
		[ExpectedException(typeof(UnityException))]
		public void TestAddItemToFullInventory() {
			InventoryItem newItem = new InventoryItem();

			fullInventoryPlayer.AddItem(newItem);
		}

		[Test]
		[ExpectedException (typeof (UnityException))]
		public void TestRemoveItemFromEmptyInventory ()
		{
			player1.InitialiseInventory(10);
			Assert.That(player1.Inventory.Count == 0);
			InventoryItem item = new InventoryItem();
			player1.RemoveItem(item);
		}

		[Test]
		public void TestRemoveItemFromFullInventory ()
		{
			Assert.That(fullInventoryPlayer.Inventory.IsFull);
			InventoryItem removed = fullInventoryPlayer.RemoveItem(fullInventoryPlayer.Inventory.Items[0]);
			Assert.That(!fullInventoryPlayer.Inventory.IsFull);
			Assert.That(!fullInventoryPlayer.Inventory.Contains(removed));
		}

		[Test]
		public void TestAddTool ()
		{
			Assert.Null(player1.CurrentTool);
			Assert.That(player1.Tools.Count == 0);
			Tool grabber1 = Grabber.Instance;
			player1.AddTool(grabber1);
			Assert.NotNull(player1.CurrentTool);
			Assert.That(player1.Tools.Count == 1);
			Assert.That(player1.CurrentTool == Grabber.Instance);
		}

		[Test]
		public void EmptyTest ()
		{
			Assert.That (1 == 1);
		}

	}
}