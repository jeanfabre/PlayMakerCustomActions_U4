// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Keywords: All Recursive Childs Children


using UnityEngine;
using System.Linq;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets the number of children recursivly ( all children, not just direct childs) that a GameObject has.")]
	public class GetChildCountRecursive : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject to test.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("if true, will count inactive gameobjects")]
		public FsmBool includeInactive;

		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the number of children in an int variable.")]
		public FsmInt storeResult;

		Transform[] _result;

		public override void Reset()
		{
			gameObject = null;
			includeInactive = null;
			storeResult = null;
		}

		public override void OnEnter()
		{
			DoGetChildCountRecursive();
			
			Finish();
		}

		void DoGetChildCountRecursive()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
			    return;
			}

			_result = go.GetComponentsInChildren<Transform>(includeInactive.Value);

			storeResult.Value = _result.Length -1;

		}
	}
}