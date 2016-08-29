using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

[System.Serializable]
public class Game {

	private Player player;
	public Player Player { get {return player;}	private set {player = value;} }

	private List<Npc> currentNpcs;
	public List<Npc> CurrentNpcs { get {return currentNpcs;}	set {currentNpcs = value;} }


	private CheckPointList checkPoints;
	public CheckPointList CheckPoints { get {return checkPoints;} }

	//Set at savetime by the SaveLoad script
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

	public override string ToString ()
	{
		StringBuilder builder = new StringBuilder();
		builder.Append (player.Name + "\n");
		builder.Append("----------------\n");
		builder.Append(saveTime.ToString());
		return builder.ToString();
	} 

	public Game Copy ()
	{
		Game copy = new Game (player.Copy ());
		foreach (Npc npc in currentNpcs) {
			copy.CurrentNpcs.Add(npc.Copy());
		}
		copy.checkPoints = CheckPoints.Copy();
		return copy;
	}
}
