// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// made by DjayDino

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if any of the given Bool Variables are False.")]
	public class BoolAnyFalse : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("The Bool variables to check.")]
		public FsmBool[] boolVariables;

        [Tooltip("Event to send if any of the Bool variables are False.")]
		public FsmEvent sendEvent;
		
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a Bool variable.")]
        public FsmBool storeResult;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

		public override void Reset()
		{
			boolVariables = null;
			sendEvent = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoAnyTrue();
			
			if (!everyFrame)
			{
			    Finish();
			}		
		}
		
		public override void OnUpdate()
		{
			DoAnyTrue();
		}
		
		void DoAnyTrue()
		{
			if (boolVariables.Length == 0)
			{
			    return;
			}
			
			storeResult.Value = false;
			
			for (var i = 0; i < boolVariables.Length; i++) 
			{
				if (boolVariables[i].Value == false)
				{
					Fsm.Event(sendEvent);
					storeResult.Value = true;
					return;
				}
			}
		}
	}
}