// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Sends an Event when a KeyCode is detected in the GUI. Always use this before a textfield action to catch enter for example")]
	public class GuiKeyboardEvent : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The key to catch")]		
		public KeyCode keyCode;

		[RequiredField]
		[Tooltip("Event to send when key detected")]
		public FsmEvent sendEvent;

		public override void Reset()
		{
			keyCode = KeyCode.Return;
			
			sendEvent = null;
		}
		
		
		public override void OnGUI()
		{
			if (UnityEngine.Event.current.type == UnityEngine.EventType.KeyDown && UnityEngine.Event.current.keyCode == keyCode) {
				Fsm.Event(sendEvent);
			}

		}
	}
}
