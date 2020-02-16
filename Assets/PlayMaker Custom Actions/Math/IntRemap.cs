// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Remap a Int from one scale to the other. example: we have 2 existing between 1 and 3, remapping 2 between 0 and 10 would give 5")]
    public class IntRemap : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The Value")]
        public FsmInt theInt;

        [RequiredField]
        [Tooltip("The base start reference")]
        public FsmInt baseStart;

        [RequiredField]
        [Tooltip("The base end reference")]
        public FsmInt baseEnd;

        [RequiredField]
        [Tooltip("The target start reference")]
        public FsmInt targetStart;

        [RequiredField]
        [Tooltip("The target end reference")]
        public FsmInt targetEnd;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result in this Int variable.")]
        public FsmInt storeResult;

        [Tooltip("Repeat every frame. Useful if any of the values are changing.")]
        public bool everyFrame;

        public override void Reset()
        {
            theInt = 50;
            baseStart = 0;
            baseEnd = 100;

            targetStart = 0;
            targetEnd = 1;

            storeResult = null;
            everyFrame = true;
        }

        public override void OnEnter()
        {
            DoIntRemap();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoIntRemap();
        }

        void DoIntRemap()
        {
            storeResult.Value = map(theInt.Value, baseStart.Value, baseEnd.Value, targetStart.Value, targetEnd.Value);
        }

        int map(int s, int a1, int a2, int b1, int b2)
        {
            return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
        }

    }
}

