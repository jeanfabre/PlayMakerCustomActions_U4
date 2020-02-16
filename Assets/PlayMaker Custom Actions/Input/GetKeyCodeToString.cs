// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Made by Djaydino http://www.jinxtergames.com
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Get KeyCode and set to string ")]
	public class GetKeyCodeToString : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString storeResult;
		
		public FsmEvent sendEvent;
		private KeyCode kcode;
		private KeyCode newcode;

		public override void Reset()
		{
			storeResult = null;
		}

		public override void OnUpdate()
		{
		detectpressedkey();
		}
		
		public void detectpressedkey()
		{
			foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
			{
			if (Input.GetKey(kcode))
			{
				newcode = kcode;
			}
				if (Input.anyKey)
				{
				storeResult.Value = newcode.ToString();
				Fsm.Event(sendEvent);
				}
				
			}
			
		}
	}
}
