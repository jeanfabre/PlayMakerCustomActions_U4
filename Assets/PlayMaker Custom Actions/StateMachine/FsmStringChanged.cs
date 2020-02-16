// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Tests if the value of a FSM String variable changed. Use this to send an event on change, or store a bool that can be used in other operations.")]
    public class FsmStringChanged : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;
        [RequiredField]
        [UIHint(UIHint.FsmString)]
        public FsmString variableName;
        string storeFsmValue;
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;
        public FsmEvent changedEvent;
        string previousValue;

        GameObject goLastFrame;
        PlayMakerFSM fsm;

        public override void Reset()
        {
            gameObject = null;
            fsmName = "";

        }

        public override void OnEnter()
        {
            Ggop();
            previousValue = storeFsmValue;
            ttop();
        }

        public override void OnUpdate()
        {
            Ggop();
            ttop();
        }



        void Ggop()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;
            if (go != goLastFrame)
            {
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
                goLastFrame = go;
            }

            if (fsm == null) return;

            FsmString fsmString = fsm.FsmVariables.GetFsmString(variableName.Value);

            if (fsmString == null) return;

            storeFsmValue = fsmString.Value;


        }
        void ttop()
        {
            if (storeFsmValue != previousValue)
            {
                storeResult.Value = true;
                Fsm.Event(changedEvent);
            }
        }
    }
}

