// Made by nightcorelv
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Inverts the sign of an FSM float variable. If the given float is positive, it gets flipped and becomes negative and vice versa.")]
    public class FsmFloatFlip : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;
        [RequiredField]
        [UIHint(UIHint.FsmFloat)]
        public FsmString variableName;
        float storeFsmValue;
        [UIHint(UIHint.Variable)]
        public FsmFloat StoreResult;
        [Tooltip("Flip FSM value,if true, that will change the FSM value")]
        public FsmBool FlipFsmValue;
        public bool everyFrame;

        GameObject goLastFrame;
        PlayMakerFSM fsm;


        public override void Reset()
        {
            FlipFsmValue = false;
            StoreResult = null;
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
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;
            if (go != goLastFrame)
            {
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
                goLastFrame = go;
            }

            if (fsm == null) return;

            FsmFloat fsmFloat = fsm.FsmVariables.GetFsmFloat(variableName.Value);

            if (fsmFloat == null) return;

            storeFsmValue = fsmFloat.Value;


            if (FlipFsmValue.Value)
            {
                fsmFloat.Value = storeFsmValue * (-1);
                StoreResult.Value = fsmFloat.Value;
            }

            else
            {
                storeFsmValue = storeFsmValue * (-1);
                StoreResult.Value = storeFsmValue;
            }
        }

    }
}