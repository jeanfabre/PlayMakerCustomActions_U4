// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made By : DjayDino

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Immediately return to the previously active state.")]
	public class GotoState : FsmStateAction
	{

        public FsmString stateName;


		public override void Reset()
		{
            stateName = null;
		}

		public override void OnEnter()
		{
			if (stateName != null)
			{
                Fsm.SetState(stateName.Value);
                Log("Goto Selected State: " + stateName.Value);
			}
			
			Finish();
		}
	}
}
