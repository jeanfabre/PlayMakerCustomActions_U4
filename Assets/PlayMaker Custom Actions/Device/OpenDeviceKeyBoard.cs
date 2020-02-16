// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Opens the native keyboard provided by OS on the screen")]
	public class OpenDeviceKeyBoard : FsmStateAction
	{
		#if UNITY_IPHONE
		[Tooltip("The KeyBoard type")]
		public TouchScreenKeyboardType keyBoardType;
		[Tooltip("AutoCorrection setting")]
		public FsmBool autoCorrection;
		[Tooltip("single or multiline setting")]
		public FsmBool multiLine;
		[Tooltip("Hides inputed text")]
		public FsmBool secure;
		[Tooltip("Swith to Alert keyboard theme")]
		public FsmBool alert;
		[Tooltip("The placeholder text")]
		public FsmString textPlaceHolder;
		
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The text the user is entering")]
		public FsmString text;
		
		[Tooltip("Event sent when keyboard becomes active. Comes back to the state to keep using the keyboard")]
		public FsmEvent active;
		
		[Tooltip("Event sent when the user is done")]
		public FsmEvent done;
		
		
		private static TouchScreenKeyboard keyboard;
		

		private bool _active = false;
		private bool _done = false;
		
		public override void Reset()
		{
			_active = false;
			_done = false;
			keyBoardType = TouchScreenKeyboardType.Default;
			autoCorrection = true;
			multiLine = false;
			secure = false;
			alert = false;
			textPlaceHolder = "";
		}
		
		public override void OnEnter()
		{
			_done = false;

			UnityEngine.Debug.Log("OpenDeviceKeyBoard OPEN");
			if (keyboard == null)
			{
				keyboard = TouchScreenKeyboard.Open(text.Value,keyBoardType,autoCorrection.Value,multiLine.Value,secure.Value,alert.Value,textPlaceHolder.Value);


			}else{
				UnityEngine.Debug.Log("OpenDeviceKeyBoard OPEN NOT NULL");
			}
		}
		
		public override void OnUpdate()
		{
			if (keyboard != null)
			{
				text.Value = keyboard.text;	
				
				
				if (!_active && keyboard.active)
				{
					_active = true;
					Fsm.Event(active);	
				}
				
				if (!_done && keyboard.done)
				{
					keyboard = null;
					_done = true;
					UnityEngine.Debug.Log("OpenDeviceKeyBoard DONE");
					Fsm.Event(done);
					Finish();
				}
				
			}
		}
		#endif
	}
}
