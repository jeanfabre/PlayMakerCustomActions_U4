// Made by nightcorelv
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Compares FSM Game Objects and sends Events based on the result.")]
    public class FsmGameObjectIsNull : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;
        [RequiredField]
        [UIHint(UIHint.FsmGameObject)]
        public FsmString variableName;
        GameObject storeFsmValue;
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;
        public FsmEvent isNull;
        public FsmEvent isNotNull;
        public bool everyFrame;

        GameObject goLastFrame;
        PlayMakerFSM fsm;


        public override void Reset()
        {
            isNull = null;
            isNotNull = null;
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
            var goIsNull = storeFsmValue == null;

            if (storeResult != null)
            {
                storeResult.Value = goIsNull;
            }

            Fsm.Event(goIsNull ? isNull : isNotNull);
        }
    }
}