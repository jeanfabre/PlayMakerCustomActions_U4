// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets if the fsm is a sub fsm.")]
	public class GetIsSubFsm : FsmStateAction
	{

		[UIHint(UIHint.Variable)]
		[Tooltip("True if the fsm is a sub fsm.")]
		public FsmBool isSubFsm;
		
		[Tooltip("Event sent if the fsm is a sub fsm.")]
		public FsmEvent isSubFsmEvent;
		
		[Tooltip("Event sent if the fsm is a NOT sub fsm.")]
		public FsmEvent isNotSubFsmEvent;
		
		public override void Reset()
		{
			isSubFsm = null;
			isSubFsmEvent = null;
			isNotSubFsmEvent = null;
		}
		
		public override void OnEnter()
		{
			bool _isSub = Fsm.IsSubFsm;
			isSubFsm = _isSub;
			
			if (_isSub)
			{
				Fsm.Event(isSubFsmEvent);
			}else{
				Fsm.Event(isNotSubFsmEvent);
			}
			
			Finish();
		}
	}
}
