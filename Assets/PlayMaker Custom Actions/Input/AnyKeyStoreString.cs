// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Sends an Event when the user hits any Key or Mouse Button.")]
	public class AnyKeyStoreString : FsmStateAction
	{
		[RequiredField]
		public FsmEvent sendEvent;
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;

		public override void Reset()
		{
			sendEvent = null;
			storeResult = null;
		}

		public override void OnUpdate()
		{
			storeResult.Value = Input.inputString;
			if (Input.anyKey)
				Fsm.Event(sendEvent);		
		}
	}
}
