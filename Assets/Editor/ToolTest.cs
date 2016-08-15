using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestSceneTests
{
	[TestFixture]
	public class ToolTest {
		private Tool toolGrabber;
		private Grabber grabber;
		private GameObject rubbishItem;

		[SetUp]
		public void Init() {
			rubbishItem = new GameObject("Rubbish Item");
			rubbishItem.AddComponent<CircleCollider2D>();
			rubbishItem.tag = "Rubbish";
			Collectable collectable = rubbishItem.AddComponent<Collectable>();
			collectable.item = new InventoryItem();

			toolGrabber = Grabber.Instance;
			grabber = Grabber.Instance;
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
			Assert.NotNull(toolGrabber.Icon);
			Assert.NotNull(toolGrabber.Sprites);

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

	}
}
