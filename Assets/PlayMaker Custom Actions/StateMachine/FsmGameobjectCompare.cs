// Made by nightcorelv
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Compares FSM Game Objects and sends Events based on the result.")]
    public class FsmGameObjectCompare : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;
        [RequiredField]
        [UIHint(UIHint.FsmGameObject)]
        public FsmString variableName;
        GameObject storeFsmValue;
        public FsmGameObject compareTo;
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;
        public FsmEvent equalEvent;
        public FsmEvent notEqualEvent;
        public bool everyFrame;

        GameObject goLastFrame;
        PlayMakerFSM fsm;


        public override void Reset()
        {
            everyFrame = false;
            storeResult = null;
            equalEvent = null;
            notEqualEvent = null;
            compareTo = null;
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
            var equal = storeFsmValue == compareTo.Value;

            storeResult.Value = equal;

            if (equal && equalEvent != null)
            {
                Fsm.Event(equalEvent);
            }
            else if (!equal && notEqualEvent != null)
            {
                Fsm.Event(notEqualEvent);
            }

        }
    }
}