// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Text.RegularExpressions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a string into a Bool variable.")]
	public class ConvertStringToBool : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The string representing the bool value.")]
		public FsmString source;
		
		[Tooltip("The expected format of the source representing the true value for a boolean. Warning: Case sensitive")]
		public FsmString trueValue;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result")]
		public FsmBool storeResult;
		
		[Tooltip("Repeats every frame.")]
		public bool everyFrame;
		
		public override void Reset()
		{
			source = null;
			trueValue = "true";
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoParsing();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoParsing();
		}
		
		void DoParsing()
		{
			if (source == null) return;
			if (storeResult == null) return;
			
			storeResult.Value = string.Equals(source.Value,trueValue.Value);
		}
		
	}
}
