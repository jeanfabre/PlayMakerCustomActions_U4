// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [Tooltip("Sends an Event based on the value of a FSM Float Variable. The float could represent distance, angle to a target, health left... The array sets up float ranges that correspond to Events.")]
    public class FsmFloatSwitch : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmFloat)]
        [Tooltip("The name of the FSM variable.")]
        public FsmString variableName;

        float StoreFsmVariable;
        [CompoundArray("Float Switches", "Less Than", "Send Event")]
        public FsmFloat[] lessThan;
        public FsmEvent[] sendEvent;
        public bool everyFrame;

        GameObject goLastFrame;
        PlayMakerFSM fsm;

        public override void Reset()
        {
            lessThan = new FsmFloat[1];
            sendEvent = new FsmEvent[1];
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

            FsmFloat fsmFloat = fsm.FsmVariables.GetFsmFloat(variableName.Value);

            if (fsmFloat == null) return;

            StoreFsmVariable = fsmFloat.Value;

            ttop();
        }

        void ttop()
        {
            for (var i = 0; i < lessThan.Length; i++)
            {
                if (StoreFsmVariable < lessThan[i].Value)
                {
                    Fsm.Event(sendEvent[i]);
                    return;
                }
            }
        }
    }
}