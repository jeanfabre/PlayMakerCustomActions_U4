// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of 2 Floats with bool storing options")]
	public class FloatCompareWithBools : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The first float variable.")]
		public FsmFloat float1;

		[RequiredField]
        [Tooltip("The second float variable.")]
		public FsmFloat float2;

		[RequiredField]
        [Tooltip("Tolerance for the Equal test (almost equal).")]
		public FsmFloat tolerance;

		[Tooltip("Event sent if Float 1 equals Float 2 (within Tolerance)")]
		public FsmEvent equal;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResultEqual;

        [Tooltip("Event sent if Float 1 is less than Float 2")]
		public FsmEvent lessThan;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResultLess;
		
        [Tooltip("Event sent if Float 1 is greater than Float 2")]
		public FsmEvent greaterThan;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResultGreater;
		
        [Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
        public bool everyFrame;
		


		public override void Reset()
		{
			float1 = 0f;
			float2 = 0f;
			tolerance = 0f;
			equal = null;
			lessThan = null;
			greaterThan = null;
			everyFrame = false;
			storeResultEqual = null;
			storeResultLess = null;
			storeResultGreater = null;
		}

		public override void OnEnter()
		{
			DoCompare();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoCompare();
		}

		void DoCompare()
		{
			if (Mathf.Abs(float1.Value - float2.Value) <= tolerance.Value)
			{
				storeResultEqual.Value = true;				
				Fsm.Event(equal);
			}
			
			else
			{
				storeResultEqual.Value = false;
			}

			if (float1.Value < float2.Value)
			{
				storeResultLess.Value = true;
				Fsm.Event(lessThan);
			}
			
			else
			{
			storeResultLess.Value = false;
			}
			

			if (float1.Value > float2.Value)
			{
				storeResultGreater.Value = true;
				Fsm.Event(greaterThan);
			}
			
			else
			{
				storeResultGreater.Value = false;
			}
		}
	}
}
