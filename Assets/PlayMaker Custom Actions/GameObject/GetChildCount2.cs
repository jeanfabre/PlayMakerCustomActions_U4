// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the number of children that a GameObject has. This version has everyframe option")]
	public class GetChildCount2 : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the number of children in an int variable.")]
		public FsmInt storeResult;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;


		public override void Reset()
		{
			gameObject = null;
			storeResult = null;
			everyFrame = false;
		}
		
		public override void OnUpdate()
		{
			DoGetChildCount();
			
			if(!everyFrame)
			{
				Finish();
			}
		}


		void DoGetChildCount()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
			    return;
			}

			storeResult.Value = go.transform.childCount;
		}
	}
}