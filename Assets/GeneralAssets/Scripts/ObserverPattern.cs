using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ObserverPattern {

	public abstract class Subject {

		private List<Observer> observers = new List<Observer>();

		public void AddObserver (Observer obs)
		{
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

