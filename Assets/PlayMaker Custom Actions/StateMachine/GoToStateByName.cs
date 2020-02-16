// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// http://hutonggames.com/playmakerforum/index.php?topic=6420.msg31329#msg31329

using UnityEngine;
using System;
using System.Reflection;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Immediately switch to a state with the selected name.")]
	public class GoToStateByName : FsmStateAction
	{
		 [RequiredField]
        [Tooltip("The GameObject that owns the FSM")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Name of FSM on Game Object. Leave to none to target this fsm")]
        public FsmString fsmName;
		
		[RequiredField]
		[Tooltip("The name of the state to go to")]
		public FsmString stateName;
		
		[Tooltip("Event Sent if the state was found")]
		public FsmEvent stateFoundEvent;
		
		[Tooltip("Event Sent if the state was not found")]
		public FsmEvent stateNotFoundEvent;
		
		public override void Reset()
		{
			gameObject = null;
			fsmName = new FsmString(){UseVariable=true};
			stateName = null;
			stateFoundEvent = null;
			stateNotFoundEvent = null;
		}
		
		public override void OnEnter()
		{
			DoGotoState();
			
			Finish();
		}
		
		void DoGotoState()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
				Fsm.Event(stateNotFoundEvent);
                return;
            }

         	Fsm sourceFsm = this.Fsm;
				
			if (!fsmName.IsNone)
			{
            	PlayMakerFSM _sourceFsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
				
				if (_sourceFsm!=null)
				{
					sourceFsm = _sourceFsm.Fsm;
				}
			}
			
			
			FsmState targetState = null;
			foreach (FsmState state in sourceFsm.States)
			{
				if (state.Name == stateName.Value)
				{
					targetState = state;
					break;
				}
			}


			if (targetState != null)
			{
				MethodInfo switchState = Fsm.GetType().GetMethod("SwitchState", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				switchState.Invoke(sourceFsm, new object[] { targetState });
				Fsm.Event(stateFoundEvent);
			}
			else
			{
				Fsm.Event(stateNotFoundEvent);
			}

			Finish();
		}
	}
}
