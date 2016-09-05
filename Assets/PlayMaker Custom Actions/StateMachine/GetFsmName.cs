// (c) copyright Hutong Games, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Get this FSM name.")]
    public class GetFsmName : FsmStateAction
    {
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the full label of the FSM.")]
		public FsmString fullLabel;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the name of the FSM.")]
		public FsmString name;
		
        public override void OnEnter()
        {
			fullLabel.Value = Fsm.GetFullFsmLabel(this.Fsm);
			name.Value = this.Fsm.Name;

			Finish ();
        }
    }
}
