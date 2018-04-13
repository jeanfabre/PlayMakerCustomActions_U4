// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
// Made by Djaydino http://www.jinxtergames.com
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when the Keystring is released. Waring!! The String must be a valid KeyCode.")]
	public class GetKeyUpFromString : FsmStateAction
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
			bool keyUp = Input.GetKeyUp(key);
			
			if (keyUp)
            {
                storeKey = keyCode;
                Fsm.Event(sendEvent);
            }
			storeResult.Value = keyUp;
		}
	}
}
