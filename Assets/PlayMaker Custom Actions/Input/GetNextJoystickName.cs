// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Each time this action is called it gets the next child Joystick. This lets you quickly loop through all the Joystick perform actions on them.")]
	public class GetNextJoystickName : FsmStateAction
	{
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the next Joystick Name")]
		public FsmString storeNextJoystickName;

		[Tooltip("Event to send to get the next Joystick.")]
		public FsmEvent loopEvent;

		[Tooltip("Event to send when there are no more Joysticks.")]
		public FsmEvent finishedEvent;

		public override void Reset()
		{
			storeNextJoystickName = null;
			loopEvent = null;
			finishedEvent = null;
		}

		// increment the index as we loop through children
		private int nextIndex;
		
		public override void OnEnter()
		{
			DoGetNextJoystick();

			Finish();
		}

		void DoGetNextJoystick()
		{
			
			// no more children?
			// check first to avoid errors.
			if (nextIndex >= Input.GetJoystickNames().Length)
			{
				nextIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}

			// get next child
			storeNextJoystickName.Value = Input.GetJoystickNames()[nextIndex];


			// no more items?
			// check a second time to avoid process lock and possible infinite loop if the action is called again.
			// Practically, this enabled calling again this state and it will start again iterating from the first joystick.
			if (nextIndex >= Input.GetJoystickNames().Length)
			{
				nextIndex = 0;
				Fsm.Event(finishedEvent);
				return;
			}

			// iterate the next Joystick
			nextIndex++;

			if (loopEvent != null)
			{
				Fsm.Event(loopEvent);
			}
		}
	}
}
