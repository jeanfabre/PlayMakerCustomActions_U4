// Made by nightcorelv
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Inverts the sign of an FSM int variable. If the given int is positive, it gets flipped and becomes negative and vice versa.")]
    public class FsmIntFlip : FsmStateAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;
        [RequiredField]
        [UIHint(UIHint.FsmInt)]
        public FsmString variableName;
        int storeFsmValue;
        [UIHint(UIHint.Variable)]
        public FsmInt StoreResult;
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

            FsmInt fsmInt = fsm.FsmVariables.GetFsmInt(variableName.Value);

            if (fsmInt == null) return;

            storeFsmValue = fsmInt.Value;


            if (FlipFsmValue.Value)
            {
                fsmInt.Value = storeFsmValue * (-1);
                StoreResult.Value = fsmInt.Value;
            }

            else
            {
                storeFsmValue = storeFsmValue * (-1);
                StoreResult.Value = storeFsmValue;
            }
        }

    }
}