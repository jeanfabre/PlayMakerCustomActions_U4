// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Check if a String variable is null or empty and sends Events based on the result.")]
	public class StringIsNullOrEmpty : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString stringVariable;
		
		public FsmEvent isNullOrEmptyEvent;
		public FsmEvent isNotNullOrEmptyEvent;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the true/false result in a bool variable.")]
		public FsmBool storeResult;
		
		[Tooltip("Repeat every frame. Useful if any the string is changing over time.")]
		public bool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			isNullOrEmptyEvent = null;
			isNotNullOrEmptyEvent = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoStringIsNullOrEmpty();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoStringIsNullOrEmpty();
		}
		
		void DoStringIsNullOrEmpty()
		{
			
			bool _isNullOrEmpty = stringVariable ==null || string.IsNullOrEmpty (stringVariable.Value);
			
			storeResult.Value = _isNullOrEmpty;
			
			if (_isNullOrEmpty)
			{
				Fsm.Event(isNullOrEmptyEvent);
			}else{
				Fsm.Event(isNotNullOrEmptyEvent);
			}

		}
		
	}
}
