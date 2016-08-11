using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game {

	private Player player;

	public Player Player {
		get {
			return player;
		}
		set {
			player = value;
		}
	}

	private List<NonPlayerCharacter> currentNpcs;


	public List<NonPlayerCharacter> CurrentNpcs {
		get {
			return currentNpcs;
		}
		set {
			currentNpcs = value;
		}
	}
}
