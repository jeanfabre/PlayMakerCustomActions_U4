// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Adds multipe int variables to int variable.")]
    public class IntAddMultiple : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("The float variables to add.")]
        public FsmInt[] intVariables;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Add to this variable.")]
        public FsmInt addTo;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        public override void Reset()
        {
            intVariables = null;
            addTo = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoIntAdd();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoIntAdd();
        }

        void DoIntAdd()
        {
            for (var i = 0; i < intVariables.Length; i++)
            {
                addTo.Value += intVariables[i].Value;
            }
        }
    }
}
