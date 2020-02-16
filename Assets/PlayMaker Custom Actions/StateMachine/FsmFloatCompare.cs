// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [Tooltip("Compare the value of a Float Variable from another FSM.")]
    public class FsmFloatCompare : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmFloat)]
        public FsmString variableName;

        [RequiredField]
        public FsmFloat value;

        public FsmEvent Equal;
        public FsmEvent LessThan;
        public FsmEvent GreaterThan;
        float StoreFsmVariable;
        public bool everyFrame;


        GameObject goLastFrame;
        PlayMakerFSM fsm;




        public override void Reset()
        {
            value = 0;
            Equal = null;
            LessThan = null;
            GreaterThan = null;
            everyFrame = false;
            gameObject = null;
            variableName = "";
        }


        public override void OnEnter()
        {
            DoGetFsmFloat();

            if (!everyFrame)
                Finish();
        }




        public override void OnUpdate()
        {
            DoGetFsmFloat();
        }

        void DoGetFsmFloat()
        {

            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;



            if (go != goLastFrame)
            {
                goLastFrame = go;
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
            }

            if (fsm == null) return;

            FsmFloat fsmFloat = fsm.FsmVariables.GetFsmFloat(variableName.Value);

            if (fsmFloat == null) return;

            StoreFsmVariable = fsmFloat.Value;

            DoFloatCompare();

        }
        void DoFloatCompare()
        {
            if (StoreFsmVariable == value.Value)
            {
                Fsm.Event(Equal);
                return;
            }

            if (StoreFsmVariable < value.Value)
            {
                Fsm.Event(LessThan);
                return;
            }

            if (StoreFsmVariable > value.Value)
            {
                Fsm.Event(GreaterThan);
            }


        }
        public override string ErrorCheck()
        {
            if (FsmEvent.IsNullOrEmpty(Equal) &&
                FsmEvent.IsNullOrEmpty(LessThan) &&
                FsmEvent.IsNullOrEmpty(GreaterThan))
                return "Action sends no events!";
            return "";
        }
    }

}
