// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
    [Tooltip("Removes all leading and trailing white-space characters from a String")]
	public class StringTrim : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringInput;
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;
		public bool everyFrame;

		public override void Reset()
		{
			stringInput = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoReplace();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoReplace();
		}
		
		void DoReplace()
		{
			if (stringInput == null) return;
			if (storeResult == null) return;

            storeResult.Value = stringInput.Value.Trim();
		}
		
	}
}