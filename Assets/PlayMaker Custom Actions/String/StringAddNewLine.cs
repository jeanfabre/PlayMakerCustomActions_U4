// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Adds strings together separated by a line.")]
	public class StringAddNewLine : FsmStateAction
	{
		[Tooltip("List of the strings to compose.")]
		[RequiredField]
		public FsmString[] stringParts;
		
		[Tooltip("Store the result.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;
		

		public override void Reset()
		{
			stringParts = new FsmString[2];
			
			storeResult = null;
		}

		public override void OnEnter()
		{
			DoBuildString();
	
			Finish();
		}

		
		void DoBuildString()
		{
			if (storeResult == null) return;
			
			string result = "";
			int count = stringParts.Length;
			for(int i=0;i<count;i++)
			{
				if (result != "")
				{
					result += "\n";
				}
				
				result += stringParts[i];	
			}

		    storeResult.Value = result;
		}
		
	}
}
