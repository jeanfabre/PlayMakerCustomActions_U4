// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Sends a Message to a Game Object with a specific variable holding reference to a fsmVector3. See Unity SendMessage docs.")]
	public class SendMessageVector3Out : FsmStateAction
	{

		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		public SendMessageOptions options;
		
		[RequiredField]
		public string functionName;
		
		[UIHint(UIHint.Variable)]
		public FsmVector3 outVector3;
		
		
		public override void Reset()
		{
			gameObject = null;
			options = SendMessageOptions.DontRequireReceiver;
			functionName = "";
		}

		public override void OnEnter()
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
				DoSendMessage(Owner);
			else
				DoSendMessage(gameObject.GameObject.Value);
			
			Finish();
		}

		void DoSendMessage(GameObject go)
		{
			//Vector3 _tmp = Vector3.zero;
			
			go.SendMessage(functionName,outVector3,options);
			
			//outVector3.Value = _tmp;
			return;
		}
	}
}
