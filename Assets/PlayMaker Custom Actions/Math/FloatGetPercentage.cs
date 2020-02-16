// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Get the percentage from a float")]
    public class FloatGetPercentage : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The Float variable to get persentage from")]
        public FsmFloat floatVariable;

        [RequiredField]
        [Tooltip("Percentage to get")]
        public FsmFloat percentage;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        [ActionSection("Result")]

        [UIHint(UIHint.Variable)]
        [Tooltip("The percentage result")]
        public FsmFloat result;


        public override void Reset()
        {
            floatVariable = null;
            percentage = null;
            everyFrame = false;
            result = null;
        }

        public override void OnEnter()
        {
            DoFloatAdd();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoFloatAdd();
        }

        void DoFloatAdd()
        {
            result.Value = (floatVariable.Value / 100) * percentage.Value;
        }
    }
}
