// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("set a new start state on an fsm.")]
	public class SetFsmStartState : FsmStateAction
	{
		[Tooltip("Drag a PlayMakerFSM component here.")]
		public PlayMakerFSM fsmComponent;
		
		[Tooltip("If not specifyng the component above, specify the GameObject that owns the FSM")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of Fsm on Game Object. If left blank it will find the first PlayMakerFSM on the GameObject.")]
		public FsmString fsmName;
		
		[RequiredField]
		[Tooltip("Store the state name in a string variable.")]
		public FsmString stateName;
		
		private PlayMakerFSM fsm;
		
		public override void Reset()
		{
			fsmComponent = null;
			gameObject = null;
			fsmName = "";
			stateName = null;
			
		}
		
		public override void Awake()
		{
			DoSetFsmState();
			Finish();
			
		}
		
		void DoSetFsmState()
		{
			if (fsm == null)
			{
				if (fsmComponent != null)
				{
					fsm = fsmComponent;
				}
				else
				{
					var go = Fsm.GetOwnerDefaultTarget(gameObject);
					if (go != null)
					{
						fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
					}
				}
				
				if (fsm == null)
				{
					LogWarning("Could not find FSM: " + fsmName.Value);
					return;
				}
				
				fsm.Fsm.StartState =  stateName.Value;
				
			}
			
		}
		
	}
}