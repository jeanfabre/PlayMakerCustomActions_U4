// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Get the number of lines of a string.")]
	public class StringGetLineCount : FsmStateAction
	{

		[Tooltip("The string.")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString theString;
		
		[Tooltip("The number of lines")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmInt lineCount;
		

		public override void Reset()
		{
			theString = null;
			lineCount = 0;
			
		}

		public override void OnEnter()
		{
			DoGetLineCountFromString();
	
			Finish();
		}

		
		void DoGetLineCountFromString()
		{
			if (theString == null) 
			{
				Debug.LogWarning("String not defined");
				return;
			}
			
			string[] split = theString.Value.Split('\n');
			
			lineCount.Value = split.Length;
		}
		
	}
}
