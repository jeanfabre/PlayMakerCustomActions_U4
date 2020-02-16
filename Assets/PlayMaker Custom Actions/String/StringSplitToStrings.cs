// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Split a string into a static number of segments at each indicated divider character. Default divider is '|' which is shift backslash.")]
	public class StringSplitToStrings : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Input string with dividers. Example: \n Frank|Tony|Bill|Jorge \n ..Making 4 result string segments.")]
		public FsmString inputString;

		[Tooltip("Divider that identifies a break between strings. **One character only.**")]
		public string divider;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Result segments stored in individual string variables.")]
		public FsmString[] results;

		public bool everyFrame;

		private Char ch;
		private string[] splitArray = null;

		public override void Reset()
		{
			inputString = null;
			divider = "|";
			results = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			ch = divider[0];

			DoStringSplit();

			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoStringSplit();
		}
		
		void DoStringSplit()
		{
			if (inputString.IsNone)
			{
				return;
			}

			splitArray = inputString.Value.Split(ch);

			for (var i = 0; i < splitArray.Length; i++) 
			{
				if (splitArray.Length > results.Length)
				{
					Debug.LogError("The String Split input string has more segments than variables to store them in.");
					break;
				}

				results[i].Value = splitArray[i];

			}
			
		}
	}
}
