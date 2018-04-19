//Made by nightcorelv

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionTarget(typeof(PlayMakerFSM), "gameObject,fsmName")]
    [ActionCategory(ActionCategory.Array)]
    [Tooltip("Gets the number of items in a FSM Array.")]
    public class FsmArrayLength : BaseFsmVariableAction
    {
        [RequiredField]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.FsmName)]
        public FsmString fsmName;

        [RequiredField]
        [UIHint(UIHint.FsmArray)]
        public FsmString variableName;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the length in an Int Variable.")]
        public FsmInt length;


        public override void Reset()
        {
            gameObject = null;
            fsmName = "";
        }

        public override void OnEnter()
        {
            ggop();
            Finish();
        }

        private void ggop()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (!UpdateCache(go, fsmName.Value))
            {
                return;
            }

            var fsmArray = fsm.FsmVariables.GetFsmArray(variableName.Value);

            length.Value = fsmArray.Length;

        }
    }
}