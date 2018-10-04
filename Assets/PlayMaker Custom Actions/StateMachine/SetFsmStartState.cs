// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Set an FSM start state.")]
	public class SetFsmStartState : FsmStateAction
    {

		[RequiredField]
		[Tooltip("The GameObject")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.FsmName)]
		[Tooltip("Optional name of FSM on Game Object")]
		public FsmString fsmName;

        [Tooltip("State name for the FSM start state")]
		public FsmString startState;

		GameObject go;
		GameObject goLastFrame;
		PlayMakerFSM fsm;

		public override void Reset ()
		{
			startState = null;
		}

        public override void OnEnter()
        {
			go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			if (go != goLastFrame)
			{
				fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
				goLastFrame = go;
			}
			
			if (fsm != null)
			{
				fsm.Fsm.StartState = startState.Value;
			}

            Finish ();
        }
    }
}