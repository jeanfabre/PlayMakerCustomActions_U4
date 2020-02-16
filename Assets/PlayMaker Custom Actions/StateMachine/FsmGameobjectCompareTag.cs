// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Compares FSM Game Objects and sends Events based on the result.")]
    public class FsmGameObjectCompareTag : FsmStateAction
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

        [RequiredField]
        [UIHint(UIHint.Tag)]
        public FsmString tag;

        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;

        public FsmEvent trueEvent;
        public FsmEvent falseEvent;
        public bool everyFrame;

        GameObject storeFsmValue;

        GameObject goLastFrame;
        PlayMakerFSM fsm;

        public override void Reset()
        {
            tag = "Untagged";
            trueEvent = null;
            falseEvent = null;
            storeResult = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            Ggop();
            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            Ggop();
        }

        void Ggop()
        {
            storeResult.Value = false;

            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;

            if (go != goLastFrame)
            {
                goLastFrame = go;
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
            }

            if (fsm == null) return;

            FsmGameObject fsmGameObject = fsm.FsmVariables.GetFsmGameObject(variableName.Value);

            if (fsmGameObject == null) return;

            storeFsmValue = fsmGameObject.Value;
            ttop();
        }

        void ttop()
        {
            var hasTag = false;

            if (storeFsmValue != null)
            {
                hasTag = storeFsmValue.CompareTag(tag.Value);
            }

            storeResult.Value = hasTag;

            Fsm.Event(hasTag ? trueEvent : falseEvent);
        }
    }
}