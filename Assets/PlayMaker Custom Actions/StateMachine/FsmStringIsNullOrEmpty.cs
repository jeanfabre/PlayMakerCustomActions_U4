//Made by nightcorelv
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [Tooltip("Compare the value of a FSM String Variable from another FSM.")]
    public class FsmStringIsNullOrEmpty : FsmStateAction
    {


        [RequiredField]
        public FsmOwnerDefault gameObject;
        [UIHint(UIHint.FsmName)]
        [Tooltip("Optional name of FSM on Game Object")]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmString)]
        public FsmString variableName;

        public FsmEvent isNullOrEmptyEvent;
        public FsmEvent isNotNullOrEmptyEvent;

        string StoreFsmVariable;
        [UIHint(UIHint.Variable)]
        public FsmBool storeResult;
        public bool everyFrame;


        GameObject goLastFrame;
        PlayMakerFSM fsm;




        public override void Reset()
        {
            isNullOrEmptyEvent = null;
            isNotNullOrEmptyEvent = null;
            everyFrame = false;
            gameObject = null;
            variableName = "";
        }


        public override void OnEnter()
        {
            ggop();

            if (!everyFrame)
                Finish();
        }




        public override void OnUpdate()
        {
            ggop();
        }

        void ggop()
        {

            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null) return;



            if (go != goLastFrame)
            {
                goLastFrame = go;
                fsm = ActionHelpers.GetGameObjectFsm(go, fsmName.Value);
            }

            if (fsm == null) return;

            FsmString fsmString = fsm.FsmVariables.GetFsmString(variableName.Value);

            if (fsmString == null) return;

            StoreFsmVariable = fsmString.Value;

            ttop();

        }


        void ttop()
        {
            bool _isNullOrEmpty = StoreFsmVariable == null || string.IsNullOrEmpty(StoreFsmVariable);

            storeResult.Value = _isNullOrEmpty;

            if (_isNullOrEmpty)
            {
                Fsm.Event(isNullOrEmptyEvent);
            }
            else
            {
                Fsm.Event(isNotNullOrEmptyEvent);
            }
        }
    }
}