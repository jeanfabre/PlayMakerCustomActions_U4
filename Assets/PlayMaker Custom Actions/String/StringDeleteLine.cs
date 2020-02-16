// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Delete a specificy line from a string. use -1 to delete the last line. else line 0 is the first line, etc")]
	public class StringDeleteLine : FsmStateAction
	{

		[Tooltip("The string.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString theString;
		
		[Tooltip("The line to delete.")]
		[RequiredField]
		public FsmInt lineIndex;
		

		public override void Reset()
		{
			theString = null;
			lineIndex = -1;
			
		}

		public override void OnEnter()
		{
			DoDeleteLineFromString();
	
			Finish();
		}

		
		void DoDeleteLineFromString()
		{
			if (theString == null) 
			{
				Debug.LogWarning("String not defined");
				return;
			}
			
			string source =  theString.Value;
			string result = "";
			
			int index = lineIndex.Value;
		
		
			string[] split = source.Split('\n');
			
			int count = split.Length;
				
			if (lineIndex.Value ==-1){
			
				index = count-1;
			}

			if (index>=count){
				return;
			}
			
			
			
			for(int i=0;i<count;i++)
			{
				if (i!=index)
				{
					
					result += split[i];
					if (i<(count-1))
					{
						result += "\n";
					}
				}
				
			}
			
			theString.Value = result;
		}
		
	}
}
