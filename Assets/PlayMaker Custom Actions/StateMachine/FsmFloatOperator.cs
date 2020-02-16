// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.StateMachine)]
    [Tooltip("Performs math operations on FSM Floats: Add, Subtract, Multiply, Divide, Min, Max.")]
    public class FsmFloatOperator : FsmStateAction
    {
        public enum Operation
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Min,
            Max
        }

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

        [RequiredField]
        public FsmFloat Value;

        public Operation operation = Operation.Add;

        [UIHint(UIHint.Variable)]
        public FsmFloat StoreResult;

        [Tooltip("Operation to original value,if true, that will change the FSM value")]
        public FsmBool ModifyFsmValue;

        public bool everyFrame;

        GameObject goLastFrame;
        PlayMakerFSM fsm;
        
        public override void Reset()
        {
            Value = null;
            ModifyFsmValue = false;
            operation = Operation.Add;
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

            var v1 = storeFsmValue;
            var v2 = Value.Value;

            if (ModifyFsmValue.Value)
            {
                switch (operation)
                {
                    case Operation.Add:
                        fsmFloat.Value = v1 + v2;
                        StoreResult.Value = fsmFloat.Value;
                        break;

                    case Operation.Subtract:
                        fsmFloat.Value = v1 - v2;
                        StoreResult.Value = fsmFloat.Value;
                        break;

                    case Operation.Multiply:
                        fsmFloat.Value = v1 * v2;
                        StoreResult.Value = fsmFloat.Value;
                        break;

                    case Operation.Divide:
                        fsmFloat.Value = v1 / v2;
                        StoreResult.Value = fsmFloat.Value;
                        break;

                    case Operation.Min:
                        fsmFloat.Value = Mathf.Min(v1, v2);
                        StoreResult.Value = fsmFloat.Value;
                        break;

                    case Operation.Max:
                        fsmFloat.Value = Mathf.Max(v1, v2);
                        StoreResult.Value = fsmFloat.Value;
                        break;
                }
            }

            else
            {
                switch (operation)
                {
                    case Operation.Add:
                        StoreResult.Value = v1 + v2;
                        break;

                    case Operation.Subtract:
                        StoreResult.Value = v1 - v2;
                        break;

                    case Operation.Multiply:
                        StoreResult.Value = v1 * v2;
                        break;

                    case Operation.Divide:
                        StoreResult.Value = v1 / v2;
                        break;

                    case Operation.Min:
                        StoreResult.Value = Mathf.Min(v1, v2);
                        break;

                    case Operation.Max:
                        StoreResult.Value = Mathf.Max(v1, v2);
                        break;
                }
            }
        }
    }
}