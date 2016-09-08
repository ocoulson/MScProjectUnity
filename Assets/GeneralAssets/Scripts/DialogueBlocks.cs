using UnityEngine;
using System;
using System.Collections;

public abstract class DialogueBlock {
	public int id{ get; set; }
	public string name{ get; set; }
	public string speaker{ get; set; }
	public string[] script_en_GB{ get; set; }

	public abstract DialogueBlock Copy();
}

[Serializable]
public class LinearDialogueBlock : DialogueBlock {

	public int nextId{ get; set; }

	public override string ToString ()
	{
		return speaker + " "+ name + " " + id + ". Next Block = " + nextId;
	}
	public override DialogueBlock Copy ()
	{
		LinearDialogueBlock copy = new LinearDialogueBlock();
		copy.id = id;
		copy.name = name;
		copy.speaker = speaker;
		copy.script_en_GB = script_en_GB;
		copy.nextId = nextId;
		return copy;
	}

}

[Serializable]
public class LinearEffectDialogueBlock : LinearDialogueBlock {
	public string effectName {get; set;}
	public override string ToString ()
	{
		return base.ToString() + ", EffectName:" + effectName;
	}
	public override DialogueBlock Copy ()
	{
		LinearEffectDialogueBlock copy = new LinearEffectDialogueBlock();
		copy.id = id;
		copy.name = name;
		copy.speaker = speaker;
		copy.script_en_GB = script_en_GB;
		copy.nextId = nextId;
		copy.effectName = effectName;
		return copy;
	}
}

[Serializable]
public class BranchDialogueBlock : DialogueBlock {

	public int yesNextId{ get; set; }
	public int noNextId{ get; set; }

	public override string ToString ()
	{
		return speaker + " "+ name + " " + id + ". YesNext: " + yesNextId + " NoNext: " + noNextId;
	}

	public override DialogueBlock Copy ()
	{
		BranchDialogueBlock copy = new BranchDialogueBlock();
		copy.id = id;
		copy.name = name;
		copy.speaker = speaker;
		copy.script_en_GB = script_en_GB;
		copy.yesNextId = yesNextId;
		copy.noNextId = noNextId;
		return copy;
	}

}

