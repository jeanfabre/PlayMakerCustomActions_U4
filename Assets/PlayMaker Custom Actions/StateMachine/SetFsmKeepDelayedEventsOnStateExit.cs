// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// keywords:
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Raise the KeepDelayedEventsOnStateExit flag so that delayed event are kept even when state exited")]
	public class SetFsmKeepDelayedEventsOnStateExit : FsmStateAction
    {
		[UIHint(UIHint.TextArea)]
        [Tooltip("If True, delayed event will be kept if the state exits")]
		public FsmBool keepDelayedEvents;

		public override void Reset ()
		{
			keepDelayedEvents = null;
		}

        public override void OnEnter()
        {
			this.Fsm.KeepDelayedEventsOnStateExit = keepDelayedEvents.Value;

            Finish ();
        }
    }
}