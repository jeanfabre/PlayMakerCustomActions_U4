// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sends an Event. If optional delay is set, WILL ONLY SEND THE DELAYED EVENT IF IT IS STILL IN THE STATE.\n" +
	 	"NOTE: 'Send Event' will fire a delayed event even if the action is not running anymore as the state exited. \n" +
	 	"NOTE: To send events between FSMs they must be marked as Global in the Events Browser.")]
	public class SendEventFromState : FsmStateAction
	{
		[Tooltip("Where to send the event.")]
		public FsmEventTarget eventTarget;
		
		[RequiredField]
		[Tooltip("The event to send. NOTE: Events must be marked Global to send between FSMs.")]
		public FsmEvent sendEvent;
		
		[HasFloatSlider(0, 10)]
		[Tooltip("Optional delay in seconds. NOTE: The event will not be fired if the state has exited before the delay")]
		public FsmFloat delay;


		private float startTime;
		
		public override void Reset()
		{
			eventTarget = null;
			sendEvent = null;
			delay = null;

		}

		public override void OnEnter()
		{
			if (delay.Value < 0.001f)
			{
				Fsm.Event(eventTarget, sendEvent);
				Finish();
			}
			else
			{
				// start a timer to know when to send the delayed event.
				startTime = Time.realtimeSinceStartup;
			
			}
		}

		
		public override void OnUpdate()
		{
			
			// check the delta time against the delay
			float deltaTime = Time.realtimeSinceStartup- startTime;
			if (deltaTime >= delay.Value)
			{
				// we fire a normal event.
				Fsm.Event(eventTarget, sendEvent);
				Finish();	
			}
		}
	}
}
