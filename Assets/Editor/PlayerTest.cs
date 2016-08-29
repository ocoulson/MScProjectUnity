using UnityEngine;
using UnityEditor;
using System;
using NUnit.Framework;

namespace RegularTests {
	[TestFixture]
	public class PlayerTest {

		private Player player1;
		private Player fullPlayer;
		private Tool tool1;
		private Tool tool2;
		private Tool tool3;

	    [SetUp]
	    public void Init ()
		{
			player1 = new Player ("Player1", Gender.FEMALE, "Eve2", new Vector2 (0, 0));
			fullPlayer = new Player ("Ollie", Gender.MALE, "Evan1", Vector2.zero);
			fullPlayer.InitialiseInventory (20);

			for (int i = 0; i < 20; i++) {
				InventoryItem item = new InventoryItem();
				item.ItemName = "Item" + i;
				item.ItemId = i;
				item.ItemType = ItemType.RUBBISH;
				item.ItemDescription = string.Empty;
				fullPlayer.AddItem(item);
			}


			tool1 = new MockTool("Tool1");
			tool2 = new MockTool("Tool2");
			tool3 = new MockTool("Tool3");

			fullPlayer.AddTool(tool1);
			fullPlayer.AddTool(tool2);
			fullPlayer.AddTool(tool3);
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
			Assert.That(fullPlayer.Inventory.Count == fullPlayer.Inventory.Size);
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

			fullPlayer.AddItem(newItem);
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
			Assert.That(fullPlayer.Inventory.IsFull);
			InventoryItem removed = fullPlayer.RemoveItem(fullPlayer.Inventory.Items[0]);
			Assert.That(!fullPlayer.Inventory.IsFull);
			Assert.That(!fullPlayer.Inventory.Contains(removed));
		}

		[Test]
		public void TestAddTool ()
		{
			Assert.Null(player1.CurrentTool);
			Assert.That(player1.Tools.Count == 0);
			Tool grabber1 = new Grabber();
			player1.AddTool(grabber1);
			Assert.NotNull(player1.CurrentTool);
			Assert.That(player1.Tools.Count == 1);
			Assert.That(player1.CurrentTool == grabber1);
		}

		[Test]
		public void TestSetCurrentToolByIndex ()
		{

			Assert.That(fullPlayer.CurrentTool == tool1);

			fullPlayer.SetCurrentTool(2);
			Assert.That(fullPlayer.CurrentTool == tool3);

			fullPlayer.SetCurrentTool(1);
			Assert.That(fullPlayer.CurrentTool == tool2);
		}

		[Test]
		[ExpectedException(typeof (IndexOutOfRangeException))]
		public void TestSetCurrentToolByIndex_OutOfRange ()
		{
			Assert.That(player1.Tools.Count == 0);
			player1.SetCurrentTool(1);
		}

		[Test]
		public void TestSetCurrentToolByName_Existing ()
		{

			Assert.That(fullPlayer.CurrentTool == tool1);

			fullPlayer.SetCurrentTool("Tool2");
			Assert.That(fullPlayer.CurrentTool == tool2);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void TestSetCurrentToolByName_NotExisting ()
		{
			Assert.That(fullPlayer.CurrentTool == tool1);
			fullPlayer.SetCurrentTool("Tool4");
		}

		[Test]
		public void EmptyTest ()
		{
			Assert.That (1 == 1);
		}
	}
}