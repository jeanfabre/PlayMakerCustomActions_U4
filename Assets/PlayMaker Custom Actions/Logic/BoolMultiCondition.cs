// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Check multiple bool variables for conditions.")]
    public class BoolMultiCondition : FsmStateAction
    {
		[CompoundArray("Conditions", "Bool", "Condition")]
        [UIHint(UIHint.Variable)]
        [Tooltip("The variables to evaluate.")]
        public FsmBool[] booleans;

		[Tooltip("The passing conditions for the above variable list. Must be the same count as Booleans.")]
		public FsmBool[] conditions;

		[Tooltip("If the evaluated booleans DO match the conditions, this event can fire.")]
		public FsmEvent passEvent;

		[Tooltip("If the evaluated booleans do NOT match the conditions, this event can fire.")]
		public FsmEvent failEvent;

		[Tooltip("true if all conditions met")]
		[UIHint(UIHint.Variable)]
		public FsmBool result;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		[Tooltip("Reports clear information about failure events.")]
		public bool debug;


		int _lastResult = -1;

        public override void Reset()
        {
			booleans = null;
			conditions = null;
			passEvent = null;
			failEvent = null;
			result = null;
            everyFrame = false;
			debug = false;
        }

        public override void OnEnter()
        {
			_lastResult = -1;

			result.Value = DoEvaluate();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
			result.Value = DoEvaluate();
        }

        bool DoEvaluate()
        {		
			if (conditions.Length != booleans.Length)
			{
				Debug.LogError ("<color=red>BoolMultiCondition Action Fatal Error:</color> The number of <i>Booleans</i> and <i>Conditions</i> must be equal!");
				Finish ();
				return false;
			}

			bool[] list = new bool[booleans.Length];

			for (var i = 0; i < booleans.Length; i++)
            {
				list[i] = (booleans[i].Value == conditions[i].Value);
				if (!list[i])
				{
					if (debug)
					{
						Debug.Log ("Failure at bool <b>Element "+i+": (" +booleans[i].Name+ ")</b> against its pass condition of <b>"+conditions[i]+".</b>");
					}

					if (_lastResult!= 0)
					{
						_lastResult = 0;
						Fsm.Event(failEvent);
					}
					return false;
				}
            }

			if (debug)
			{
				Debug.Log ("All " +list.Length + " boolean conditions passed.");
			}

			if (_lastResult!= 1)
			{
				_lastResult = 1;
				Fsm.Event(passEvent);
			}

			return true;
        }
    }
}





