// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
//Made by nightcorelv
// forumlink : http://hutonggames.com/playmakerforum/index.php?topic=18563.0

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Math)]
    [Tooltip("Performs math operations on FSM Ints: Add, Subtract, Multiply, Divide, Min, Max.")]
    public class FsmIntOperator : FsmStateAction
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
        [UIHint(UIHint.FsmInt)]
        [Tooltip("The name of the FSM variable.")]
        public FsmString variableName;

        int storeFsmValue;

        [RequiredField]
        public FsmInt Value;
        public Operation operation = Operation.Add;
        [UIHint(UIHint.Variable)]
        public FsmInt StoreResult;
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

            FsmInt fsmInt = fsm.FsmVariables.GetFsmInt(variableName.Value);

            if (fsmInt == null) return;

            storeFsmValue = fsmInt.Value;




            var v1 = storeFsmValue;
            var v2 = Value.Value;

            if (ModifyFsmValue.Value)
            {
                switch (operation)
                {
                    case Operation.Add:
                        fsmInt.Value = v1 + v2;
                        StoreResult.Value = fsmInt.Value;
                        break;

                    case Operation.Subtract:
                        fsmInt.Value = v1 - v2;
                        StoreResult.Value = fsmInt.Value;
                        break;

                    case Operation.Multiply:
                        fsmInt.Value = v1 * v2;
                        StoreResult.Value = fsmInt.Value;
                        break;

                    case Operation.Divide:
                        fsmInt.Value = v1 / v2;
                        StoreResult.Value = fsmInt.Value;
                        break;

                    case Operation.Min:
                        fsmInt.Value = Mathf.Min(v1, v2);
                        StoreResult.Value = fsmInt.Value;
                        break;

                    case Operation.Max:
                        fsmInt.Value = Mathf.Max(v1, v2);
                        StoreResult.Value = fsmInt.Value;
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