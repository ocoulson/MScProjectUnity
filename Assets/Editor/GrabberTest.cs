using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestSceneTests
{
	[TestFixture]
	public class GrabberTest {
		private Tool toolGrabber;
		private Grabber grabber;
		private GameObject rubbishItem;

		[SetUp]
		public void Init() {
			
			rubbishItem = GetRubbishItem("Rubbish Item");
			toolGrabber = Grabber.Instance;
			grabber = Grabber.Instance;
		}

		private GameObject GetRubbishItem(string name) {
			GameObject rubbishItem = new GameObject(name);
			rubbishItem.AddComponent<CircleCollider2D>();
			rubbishItem.tag = "Rubbish";
			Collectable collectable = rubbishItem.AddComponent<Collectable>();
			collectable.item = new InventoryItem();

			return rubbishItem;
		}

		[TearDown]
		public void Finish ()
		{
			grabber.InteractionObjects = new List<GameObject>();
			GameObject.DestroyImmediate(rubbishItem);
		}
	
		[Test]
		public void TestGrabberSingletonConstructor()  {
			Assert.That(toolGrabber.toolName == "Grabber1");


			Assert.NotNull(grabber.InteractionObjects);
			Assert.That(grabber.InteractionObjects.Count == 0);
		}

		[Test]
		public void TestTriggerEnter2DImpl ()
		{	
			Assert.That(grabber.InteractionObjects.Count == 0);
			grabber.OnTriggerEnter2DImpl(rubbishItem.GetComponent<CircleCollider2D>());
			Assert.That(grabber.InteractionObjects.Count == 1);

		}

		[Test]
		public void TestTriggerExit2DImpl() {
			Assert.That(grabber.InteractionObjects.Count == 0);
			grabber.OnTriggerEnter2DImpl(rubbishItem.GetComponent<CircleCollider2D>());
			Assert.That(grabber.InteractionObjects.Count == 1);

			grabber.OnTriggerExit2DImpl(rubbishItem.GetComponent<CircleCollider2D>());
			Assert.That(grabber.InteractionObjects.Count == 0);
		}

//		[Test]
//		public void TestUseWhenRubbishInRange ()
//		{
//			GameObject rubbishItem2 = GetRubbishItem ("Rubbish Item 2");
//			GameObject rubbishItem3 = GetRubbishItem ("Rubbish Item 3");
//
//			grabber.OnTriggerEnter2DImpl (rubbishItem.GetComponent<CircleCollider2D> ());
//			grabber.OnTriggerEnter2DImpl (rubbishItem2.GetComponent<CircleCollider2D> ());
//			grabber.OnTriggerEnter2DImpl (rubbishItem3.GetComponent<CircleCollider2D> ());
//			Assert.That (grabber.InteractionObjects.Count == 3);
//
//			InventoryItem item = grabber.Use ();
//			Assert.That (grabber.InteractionObjects.Count == 2);
//
//			bool containsItem = false;
//			foreach (GameObject go in grabber.InteractionObjects) {
//				if (go.GetComponent<Collectable> ().item == item) {
//					containsItem = true;
//				}
//			}
//			Assert.That(containsItem == false);
//
//			GameObject.DestroyImmediate(rubbishItem2);
//			GameObject.DestroyImmediate(rubbishItem3);
//
//
//		}
//
//		[Test]
//		[ExpectedException (typeof (UnityException))]
//		public void TestUseWhenNoRubbishInRange() {
//			Assert.That(grabber.InteractionObjects.Count == 0);
//			grabber.Use();
//		}

	}
}
