// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends an Event when the mobile device stops shaking ( actually, when it falls under a shaking threshold).")]
	public class DeviceStillEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Amount of acceleration under which to trigger the event. Use low numbers.")]
		public FsmFloat shakeThreshold;
		
		[RequiredField]
		[Tooltip("Event to send when Shake Threshold is exceded.")]
		public FsmEvent sendEvent;

		public override void Reset()
		{
			shakeThreshold = 0.3f;
			sendEvent = null;
		}

		public override void OnUpdate()
		{
			var acceleration = Input.acceleration;
			
			if (acceleration.sqrMagnitude < (shakeThreshold.Value * shakeThreshold.Value))
			{
				Fsm.Event(sendEvent);
			}
		}
	}
}
