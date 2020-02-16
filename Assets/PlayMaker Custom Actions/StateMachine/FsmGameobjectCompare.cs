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
    public class FsmGameObjectCompare : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject that owns the FSM.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmGameObject)]
        [Tooltip("The name of the FSM variable.")]
        public FsmString variableName;

        public FsmGameObject compareTo;

        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;

        public FsmEvent equalEvent;
        public FsmEvent notEqualEvent;
        public bool everyFrame;

        GameObject storeFsmValue;

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