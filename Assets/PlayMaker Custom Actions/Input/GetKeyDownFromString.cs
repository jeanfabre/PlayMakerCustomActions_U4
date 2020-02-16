// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Made by Djaydino http://www.jinxtergames.com
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when the Keystring is pressed. Waring!! The String must be a valid KeyCode.")]
	public class GetKeyDownFromString : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The keystring to test. Waring!! The String must be a valid KeyCode.")]
		public FsmString keyCode;
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmBool storeResult;
        [UIHint(UIHint.Variable)]
        public FsmString storeKey;
        private KeyCode key;
		
		public override void Reset()
		{
			sendEvent = null;
			keyCode = null;
			storeResult = null;
            storeKey = null;

        }

		public override void OnEnter()
		{
			key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyCode.Value) ;
		}
		
		public override void OnUpdate()
		{
			bool keyDown = Input.GetKeyDown(key);
			
			if (keyDown)
            {
                storeKey.Value = keyCode.Value;
                Fsm.Event(sendEvent);
                
            }
			storeResult.Value = keyDown;
		}
	}
}
