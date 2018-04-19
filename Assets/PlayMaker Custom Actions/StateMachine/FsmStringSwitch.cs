﻿//Made by nightcorelv
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [Tooltip("Sends an Event based on the FSM value of a String Variable.")]
    public class FsmStringSwitch : FsmStateAction
    {


        [RequiredField]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmString)]
        public FsmString variableName;

        [CompoundArray("String Switches", "Compare String", "Send Event")]
        public FsmString[] compareTo;
        public FsmEvent[] sendEvent;

        string StoreFsmVariable;
        public bool everyFrame;


        GameObject goLastFrame;
        PlayMakerFSM fsm;




        public override void Reset()
        {
            everyFrame = false;
            gameObject = null;
            compareTo = new FsmString[1];
            sendEvent = new FsmEvent[1];
            variableName = "";
        }


        public override void OnEnter()
        {
            Ggop();

            if (!everyFrame)
                Finish();
        }

        public override void OnUpdate()
        {
            Ggop();
        }


        void Ggop()
        {

            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;



            if (go != goLastFrame)
            {
                goLastFrame = go;
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
            }

            if (fsm == null) return;

            FsmString fsmString = fsm.FsmVariables.GetFsmString(variableName.Value);

            if (fsmString == null) return;

            StoreFsmVariable = fsmString.Value;

            ttop();

        }


        void ttop()
        {
            if (StoreFsmVariable=="")
                return;

            for (int i = 0; i < compareTo.Length; i++)
            {
                if (StoreFsmVariable == compareTo[i].Value)
                {
                    Fsm.Event(sendEvent[i]);
                    return;
                }
            }
        }


    }
}