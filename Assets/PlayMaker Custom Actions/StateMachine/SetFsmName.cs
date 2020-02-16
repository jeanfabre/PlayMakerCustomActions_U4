// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
//Action by Marc Saubion for Playmaker's Ecosystem// http://hutonggames.com/playmakerforum/index.php?topic=18387.msg79738
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Set this FSM name.")]
    public class SetFsmName : FsmStateAction
    {
		[UIHint(UIHint.TextArea)]
        [Tooltip("New name for this FSM")]
		public FsmString name;

		public override void Reset ()
		{
			name = null;
		}

        public override void OnEnter()
        {
            this.Fsm.Name = name.Value;

            Finish ();
        }
    }
}