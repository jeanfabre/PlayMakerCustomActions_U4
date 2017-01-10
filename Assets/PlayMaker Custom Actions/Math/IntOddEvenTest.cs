// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Return if a given Int is odd or even. Store result in boolean variable and/or send events.")]
	public class IntOddEvenTest : FsmStateAction
	{
		public FsmInt value;

		[UIHint(UIHint.Variable)]
		public FsmBool isOdd;

		[UIHint(UIHint.Variable)]
		public FsmBool isEven;

		public FsmEvent isOddEvent;

		public FsmEvent isEventEvent;

		public bool everyFrame;
		
		public override void Reset()
		{
			value = null;
			isOdd = null;
			isEven = null;
			isOddEvent = null;
			isEventEvent = null;
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoCheck();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoCheck();
		}
		
		void DoCheck()
		{
			bool _isOdd = value.Value % 2 != 0;

			if (!isOdd.IsNone)
			{
				isOdd.Value = _isOdd;
			}
			if (isEven.IsNone)
			{
				isEven.Value = ! _isOdd;
			}
			if (isOddEvent!=null && _isOdd)
			{
				Fsm.Event(isOddEvent);
			}
			if(isEventEvent!=null & !_isOdd)
			{
				Fsm.Event(isEventEvent);
			}

		}
	}
}