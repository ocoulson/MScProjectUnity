using UnityEngine;
using System;
using System.Collections;


public interface DialogueBlock {
	int id{ get; set; }
	string name{ get; set; }
	string speaker{ get; set; }
	string[] script_en_GB{ get; set; }


}

public class LinearDialogueBlock : DialogueBlock {
	public int id{ get; set; }
	public string name{ get; set; }
	public string speaker{ get; set; }
	public string[] script_en_GB{ get; set; }

	public int nextId{ get; set; }

	public override string ToString ()
	{
		return speaker + " "+ name + " " + id + ". Next Block = " + nextId;
	}


}

public class LinearEffectDialogueBlock : LinearDialogueBlock {
	public string effectName {get; set;}
	public override string ToString ()
	{
		return base.ToString() + ", EffectName:" + effectName;
	}
}

public class BranchDialogueBlock : DialogueBlock {
	public int id{ get; set; }
	public string name{ get; set; }
	public string speaker{ get; set; }
	public string[] script_en_GB{ get; set; }

	public int yesNextId{ get; set; }
	public int noNextId{ get; set; }

	public override string ToString ()
	{
		return speaker + " "+ name + " " + id + ". YesNext: " + yesNextId + " NoNext: " + noNextId;
	}


}

