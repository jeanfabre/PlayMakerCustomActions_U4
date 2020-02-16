// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event after an optional delay based on a Bool variable. NOTE: To send events between FSMs they must be marked as Global in the Events Browser.")]
	public class SendEventIfBool : FsmStateAction
	{
		[ActionSection("    Bool Options")]

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variable to test.")]
		public FsmBool boolVariable;

		[Tooltip("Indicate to fire the event if the Bool variable is True or False.")]
		public FsmBool testBool;
					
		[ActionSection("    Event Options")]

		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;
		
		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;
		
		[HasFloatSlider(0, 10)]
		[Tooltip("Optional delay in seconds.")]
		public FsmFloat delay;

		[Tooltip("Repeat every frame. Rarely needed. Doesn't work with Delayed Event.")]
		public bool everyFrame;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			boolVariable = null;
			testBool = true;
			eventTarget = null;
			sendEvent = null;
			delay = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			// i think this is... 
			// more flexible than separate True or False events 
			// because you can control it at runtime easily, with a bool.
			if (boolVariable.Value == testBool.Value)
			{
				DoEvent();
			}

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (boolVariable.Value == testBool.Value)
			{
				DoEvent();
			}
		}

		public void DoEvent()
		{
			if (!everyFrame && DelayedEvent.WasSent(delayedEvent))
			{
				Finish();
			}

			if (delay.Value < 0.001f)
			{
				Fsm.Event(eventTarget, sendEvent);
			}

			else
			{
				delayedEvent = Fsm.DelayedEvent(eventTarget, sendEvent, delay.Value);
			}
		}
	}
}
