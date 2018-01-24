// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Compares 2 Strings and sends Events based on the result.")]
	public class BoolCompare : FsmStateAction
	{
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Bool variable to test.")]
        public FsmBool boolVariable;
        public FsmBool compareTo;
		public FsmEvent equalEvent;
		public FsmEvent notEqualEvent;
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the true/false result in a bool variable.")]
		public FsmBool storeResult;
		[Tooltip("Repeat every frame. Useful if any of the strings are changing over time.")]
		public bool everyFrame;

		public override void Reset()
		{
            boolVariable = null;
			compareTo = null;
			equalEvent = null;
			notEqualEvent = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoStringCompare();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoStringCompare();
		}
		
		void DoStringCompare()
		{
			if (boolVariable == null || compareTo == null) return;
			
			var equal = boolVariable.Value == compareTo.Value;

			if (storeResult != null)
			{
				storeResult.Value = equal;
			}

			if (equal && equalEvent != null)
			{
				Fsm.Event(equalEvent);
				return;
			}

			if (!equal && notEqualEvent != null)
			{
				Fsm.Event(notEqualEvent);
			}

		}
		
	}
}
