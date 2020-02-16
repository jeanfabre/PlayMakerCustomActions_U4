// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets a Joystick name by its index.")]
	public class GetJoystickName : FsmStateAction
	{
	[RequiredField]
		public FsmInt joystickIndex;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the Joystick Name found at that index")]
		public FsmString joystickName;

		[Tooltip("Event to send when the index is found")]
		public FsmEvent foundEvent;

		[Tooltip("Event to send when the index is out of range")]
		public FsmEvent notFoundEvent;

		public override void Reset()
		{
			joystickIndex = 0;
			joystickName = null;
			foundEvent = null;
			notFoundEvent = null;
		}

		public override void OnEnter()
		{
			DoGetJoystick();

			Finish();
		}

		void DoGetJoystick()
		{
			
			string[] _names =	Input.GetJoystickNames();
			
			if (joystickIndex.Value<0 || joystickIndex.Value>=_names.Length)
			{
				Fsm.Event(notFoundEvent);
			}else{
				joystickName.Value = _names[joystickIndex.Value];
				Fsm.Event(foundEvent);
			}
			

		}
	}
}
