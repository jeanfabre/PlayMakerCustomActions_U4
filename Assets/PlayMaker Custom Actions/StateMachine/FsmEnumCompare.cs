// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [Tooltip("Compare the value of an Enum Variable from another FSM.")]
    public class FsmEnumCompare : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmEnum)]
        public FsmString variableName;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmEnum CompareToEnum;

        [UIHint(UIHint.Variable)]
        public FsmEnum StoreFsmEnum;

        public FsmEvent equalEvent;

        public FsmEvent notEqualEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the true/false result in a bool variable.")]
        public FsmBool storeResult;

        public bool everyFrame;

        GameObject goLastFrame;
        PlayMakerFSM fsm;


        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
            StoreFsmEnum = null;
            CompareToEnum = null;
            equalEvent = null;
            notEqualEvent = null;
            storeResult = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            ggop();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            ggop();
        }

        void ggop()
        {

            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

            if (go != goLastFrame)
            {
                goLastFrame = go;
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
            }

            if (fsm == null) return;

            var fsmEnum = fsm.FsmVariables.GetFsmEnum(variableName.Value);
            if (fsmEnum == null) return;

            StoreFsmEnum.Value = fsmEnum.Value;

            //compare


            var equal = Equals(StoreFsmEnum.Value, CompareToEnum.Value);

            if (storeResult != null)
            {
                storeResult.Value = equal;
            }

            if (equal && equalEvent != null)
            {
                Fsm.Event(equalEvent);
                return;
            }

            if (!equal && notEqualEvent != null)
            {
                Fsm.Event(notEqualEvent);
            }
        }
    }
}