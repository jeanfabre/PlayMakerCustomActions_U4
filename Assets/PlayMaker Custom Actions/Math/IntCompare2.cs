// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Performs comparision on 2 Integers: ==, >,<,>=,<=,half,twice,opposite, OppositeSign. Allow the result to be saved in a FsmBool, and can send events.")]
	public class IntCompare2 : FsmStateAction
	{
		public enum Operation
		{
			Equal,
			Greater,
			GreaterOrEqual,
			Less,
			LessOrEqual,
			OppositeSign,
			Opposite,
			Half,
			Double,
		}
		
		
		[RequiredField]
		[Tooltip("The first integer to compare")]
		public FsmInt integer1;
		
		[RequiredField]
		[Tooltip("The second integer to compare")]
		public FsmInt integer2;
		
		[Tooltip("The comparison method")]
		public Operation operation;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("The comparison result")]
		public FsmBool result;
		
		[Tooltip("Event sent if comparison is true")]
		public FsmEvent comparisonPassEvent;
		
		[Tooltip("Event sent if comparison is false")]
		public FsmEvent comparisonFailEvent;
		
		[Tooltip("Performs comparison everyframe, usefull when value is changing")]
		public bool everyFrame;

		public override void Reset()
		{
			integer1 = null;
			integer2 = null;
			operation = Operation.Equal;
			result = null;
			comparisonPassEvent = null;
			comparisonFailEvent = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoIntCompare();
			
			if (!everyFrame)
				Finish();
		}
		
		// NOTE: very frame rate dependent!
		public override void OnUpdate()
		{
			DoIntCompare();
		}
		
		void DoIntCompare()
		{
			int v1 = integer1.Value;
			int v2 = integer2.Value;
			
			bool _result = false;
			
			switch (operation)
			{
				case Operation.Equal:
				
					_result = v1 == v2;
					break;

				case Operation.Greater:
					_result = v1 > v2;
					break;

				case Operation.GreaterOrEqual:
					_result = v1 >= v2;
					break;

				case Operation.Less:
					_result = v1 < v2;
					break;

				case Operation.LessOrEqual:
					_result = v1 <= v2;
					break;

				case Operation.Opposite:
					_result = v1 == v2*-1;
					break;
				
				case Operation.OppositeSign:
					_result = v1*v2 < 0;
					break;
			}
			
			if (integer1 == null)
			{
				Debug.Log("integer1 is null");
			}
			
			result.Value = _result;
			if (_result)
			{
				 Fsm.Event(comparisonPassEvent);
			}else{
				Fsm.Event(comparisonFailEvent);
			}
		}
	}
}
