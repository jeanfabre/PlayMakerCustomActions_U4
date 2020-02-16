// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Check if a GameObject has a RigidBody attached. Sends events based on result ( Found or not found).")]
	public class HasRigidBody : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject to check.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
        [Tooltip("True if a RigidBody was found")]
		public FsmBool found;
		
		public FsmEvent foundEvent;
		
		public FsmEvent notFoundEvent;

		public override void Reset()
		{
			gameObject = null;
			
			found = null;
			
			foundEvent = null;
			
			notFoundEvent = null;
		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			
			bool _found = go!=null && go.GetComponent<Rigidbody>() !=null;
			
			found.Value =_found;
			
			if (_found)
			{
				Fsm.Event(foundEvent);
			}else{
				Fsm.Event(notFoundEvent);
			}
			
			Finish();
		}
	}
}
