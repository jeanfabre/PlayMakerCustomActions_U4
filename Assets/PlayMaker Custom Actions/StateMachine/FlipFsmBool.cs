// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Flips the value of a Bool Variable in another FSM.")]
    public class FlipFsmBool : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmBool)]
        [Tooltip("The name of the FSM variable.")]
        public FsmString variableName;

        GameObject goLastFrame;
        string fsmNameLastFrame;

        PlayMakerFSM fsm;

        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
           
        }

        public override void OnEnter()
        {
            DoFlipFsmBool();

           
        }

        void DoFlipFsmBool()
        {
          

            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            // FIX: must check as well that the fsm name is different.
            if (go != goLastFrame || fsmName.Value != fsmNameLastFrame)
            {
                goLastFrame = go;
                fsmNameLastFrame = fsmName.Value;
                // only get the fsm component if go or fsm name has changed

                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
            }

            if (fsm == null)
            {
                LogWarning("Could not find FSM: " + fsmName.Value);
                return;
            }

            var fsmBool = fsm.FsmVariables.FindFsmBool(variableName.Value);

           
            {
                fsmBool.Value = !fsmBool.Value;
                Finish();
            } 
        }



    }
}