// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Sends an Event based on the value of a Float Variable.")]
    public class FloatSwitch2 : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The float variable to test.")]
        public FsmFloat floatVariable;

        [CompoundArray("Float Switches", "Equals", "Send Event")]
        public FsmFloat[] compareTo;
        public FsmEvent[] sendEvent;

        [Tooltip("Repeat every frame. Useful if the variable is changing.")]
        public bool everyFrame;

        public override void Reset()
        {
            floatVariable = null;
            compareTo = new FsmFloat[1];
            sendEvent = new FsmEvent[1];
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoFloatSwitch();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoFloatSwitch();
        }

        void DoFloatSwitch()
        {
            if (floatVariable.IsNone)
            {
                return;
            }

            for (int i = 0; i < compareTo.Length; i++)
            {
                if (floatVariable.Value == compareTo[i].Value)
                {
                    Fsm.Event(sendEvent[i]);
                    return;
                }
            }
        }
    }
}