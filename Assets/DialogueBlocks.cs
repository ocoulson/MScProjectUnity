using UnityEngine;
using System;
using System.Collections;

namespace DialogueBlocks
{
	public abstract class DialogueBlock {
		public int id{ get; set; }
		public string name{ get; set; }
		public string speaker{ get; set; }
		public string[] script_en_GB{ get; set; }

		public override string ToString ()
		{
			return id + " " + name + " " + speaker;
		}

	}

	public class LinearDialogueBlock : DialogueBlock {
		public int nextId{ get; set; }


	}

	public class BranchDialogueBlock : DialogueBlock {
		public int yesNext{ get; set; }
		public int noNext{ get; set; }

	}
}

