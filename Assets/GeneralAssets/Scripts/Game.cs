using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Game {

	private Player player;
	public Player Player { get {return player;}	private set {player = value;} }

	private List<Npc> currentNpcs;
	public List<Npc> CurrentNpcs { get {return currentNpcs;}	set {currentNpcs = value;} }


	private CheckPointList checkPoints;
	public CheckPointList CheckPoints { get {return checkPoints;} }

	private DateTime saveTime;

	public DateTime SaveTime { get {return saveTime;}	set {saveTime = value;} }

	public Game (Player player)
	{
		this.Player = player;
		CurrentNpcs = new List<Npc>();
		checkPoints = new CheckPointList();
	}

	public void AddNpc(Npc newNpc) {
		CurrentNpcs.Add(newNpc);
	}


}
