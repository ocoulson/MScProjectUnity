using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Game {

	private PlayerModel player;
	public PlayerModel Player {
		get {
			return player;
		}
		private set {
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


	private CheckPointList checkPoints;

	public Game (PlayerModel player)
	{
		this.Player = player;
		CurrentNpcs = new List<NonPlayerCharacter>();
		checkPoints = new CheckPointList();
	}


}
