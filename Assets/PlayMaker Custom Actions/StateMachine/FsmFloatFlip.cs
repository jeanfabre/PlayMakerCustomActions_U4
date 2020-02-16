// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Inverts the sign of an FSM float variable. If the given float is positive, it gets flipped and becomes negative and vice versa.")]
    public class FsmFloatFlip : FsmStateAction
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