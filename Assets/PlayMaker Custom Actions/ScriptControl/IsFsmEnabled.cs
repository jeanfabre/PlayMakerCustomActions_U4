// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Gets the Enabled state of a Fsm Component.")]
	public class IsFsmEnabled : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;
		
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the enable state of the targeted fsm")]
		public FsmBool isEnabled;

        [Tooltip("Event to send if Fsm is enabled")]
		public FsmEvent isEnabledEvent;

        [Tooltip("Event to send if Fsm is disabled")]
		public FsmEvent isDisabledEvent;
		
		public bool everyFrame;

		PlayMakerFSM fsm;
		
		public override void Reset()
		{
			gameObject = null;
			fsmName = "";
			isEnabled = null;
			isEnabledEvent = null;
			isDisabledEvent = null;

		}

		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
			}
			
			DoGetFsmEnableState();

			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoGetFsmEnableState();
		}

		void DoGetFsmEnableState()
		{
			if (fsm == null) return;
			
			
			bool _enabled = fsm.enabled;
			isEnabled.Value = _enabled;
			if (_enabled)
			{
				Fsm.Event(isEnabledEvent);
			}else{
				Fsm.Event(isDisabledEvent);
			}
		}



	}
}
