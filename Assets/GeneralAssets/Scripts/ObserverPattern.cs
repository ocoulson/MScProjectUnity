using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ObserverPattern {
	[System.Serializable]
	public abstract class Subject {

		[System.NonSerialized]
		private List<Observer> observers = null;

		public void AddObserver (Observer obs)
		{
			if (observers == null) {
				observers = new List<Observer>();
			}
			if (!observers.Contains (obs)) {
				observers.Add(obs);
			}

		}

		public void RemoveObserver (Observer obs)
		{
			if (observers.Contains (obs)) {
				observers.Remove(obs);
			}
		}

		public void Notify ()
		{
			foreach (Observer ob in observers) {
				ob.OnNotify();
			}
		}
	}

	public interface Observer {
		void OnNotify();
	}






}

