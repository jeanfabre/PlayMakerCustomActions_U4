// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Made by Djaydino http://www.jinxtergames.com
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the pressed state of the Keystring. Waring!! The String must be a valid KeyCode.")]
	public class GetKeyFromString : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The keystring to test. Waring!! The String must be a valid KeyCode.")]
		public FsmString keyCode;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store if the key is down (True) or up (False).")]
		public FsmBool storeResult;
		
		[Tooltip("Repeat every frame. Useful if you're waiting for a key press/release.")]
		public bool everyFrame;
		private KeyCode key;
		
		public override void Reset()
		{
			keyCode = null;
			storeResult = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyCode.Value) ;
			DoGetKey();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		

		public override void OnUpdate()
		{
			DoGetKey();
		}
		
		void DoGetKey()
		{
			storeResult.Value = Input.GetKey(key);
		}
		
	}
}
