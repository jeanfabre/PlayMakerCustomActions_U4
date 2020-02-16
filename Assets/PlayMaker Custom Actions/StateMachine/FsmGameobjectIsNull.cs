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
    public class FsmGameObjectIsNull : FsmStateAction
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

        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;
        public FsmEvent isNull;
        public FsmEvent isNotNull;
        public bool everyFrame;

        GameObject storeFsmValue;
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